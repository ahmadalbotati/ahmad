-- Q1, Q2, Q3, Q5, Q8 (مدمجة)
DECLARE @val DECIMAL(18,2), @id INT = 1, @res INT;

-- Spending check
SELECT @val = SUM(list_price*quantity*(1-discount)) FROM sales.order_items oi JOIN sales.orders o ON oi.order_id=o.order_id WHERE o.customer_id=@id;
PRINT CASE WHEN @val > 5000 THEN 'VIP' ELSE 'Regular' END;

-- Inventory & Exist check
SELECT @res = quantity FROM production.stocks WHERE product_id=@id AND store_id=1;
IF @res > 20 PRINT 'Full' ELSE IF @res > 10 PRINT 'Mid' ELSE PRINT 'Low';

IF EXISTS(SELECT 1 FROM sales.customers WHERE customer_id=5) 
   SELECT COUNT(*) FROM sales.orders WHERE customer_id=5;



   -- Q9: Shipping (Scalar)
CREATE FUNCTION fn_Ship(@t DECIMAL) RETURNS DECIMAL AS 
BEGIN RETURN CASE WHEN @t>100 THEN 0 WHEN @t>50 THEN 5.99 ELSE 12.99 END END;

-- Q10: Price Range (Inline Table)
CREATE FUNCTION fn_Range(@min DECIMAL, @max DECIMAL) RETURNS TABLE AS 
RETURN (SELECT * FROM production.products WHERE list_price BETWEEN @min AND @max);

-- Q12: Discount (Scalar)
CREATE FUNCTION fn_Disc(@q INT) RETURNS INT AS 
BEGIN RETURN CASE WHEN @q>=10 THEN 15 WHEN @q>=6 THEN 10 WHEN @q>=3 THEN 5 ELSE 0 END END;


-- Q14: Restock
CREATE PROC sp_Restock @sid INT, @pid INT, @q INT, @old INT OUT, @new INT OUT AS
BEGIN
    SELECT @old = quantity FROM production.stocks WHERE store_id=@sid AND product_id=@pid;
    UPDATE production.stocks SET quantity += @q WHERE store_id=@sid AND product_id=@pid;
    SELECT @new = quantity FROM production.stocks WHERE store_id=@sid AND product_id=@pid;
END;

-- Q15: Transactional Order
CREATE PROC sp_NewOrder @cid INT, @pid INT, @q INT AS
BEGIN TRY
    BEGIN TRAN;
        -- Insert logic here
    COMMIT;
END TRY
BEGIN CATCH ROLLBACK; PRINT ERROR_MESSAGE(); END CATCH;



-- Q16: Search
CREATE PROC sp_Search @name VARCHAR(50) = '%', @sort VARCHAR(20) = 'product_name' AS
BEGIN
    DECLARE @sql NVARCHAR(MAX) = 'SELECT * FROM production.products WHERE product_name LIKE @n ORDER BY ' + @sort;
    EXEC sp_executesql @sql, N'@n VARCHAR(50)', @name;
END;