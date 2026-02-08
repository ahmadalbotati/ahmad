-- Q1, Q2, Q4, Q5 (مدمجة)
SELECT product_name, 
    CASE WHEN list_price < 300 THEN 'Economy' WHEN list_price < 1000 THEN 'Standard' ELSE 'Luxury' END AS Cat,
    ISNULL(phone, 'N/A') as Phone,
    COALESCE(phone, email, 'No Contact') as Preferred,
    ISNULL(list_price / NULLIF(quantity, 0), 0) as PricePerUnit
FROM production.products p 
LEFT JOIN production.stocks s ON p.product_id = s.product_id;

-- Q3: Staff Level
SELECT staff_id, CASE WHEN COUNT(order_id) > 25 THEN 'Expert' WHEN COUNT(order_id) > 10 THEN 'Senior' ELSE 'Junior' END 
FROM sales.orders GROUP BY staff_id;



-- Q7 & Q11: Spending & Ranking
WITH Spent AS (
    SELECT customer_id, SUM(list_price*quantity*(1-discount)) AS Total,
    RANK() OVER(ORDER BY SUM(list_price*quantity*(1-discount)) DESC) as rnk,
    NTILE(5) OVER(ORDER BY SUM(list_price*quantity*(1-discount)) DESC) as Tier
    FROM sales.order_items oi JOIN sales.orders o ON oi.order_id = o.order_id GROUP BY customer_id
)
SELECT * FROM Spent WHERE Total > 1500;

-- Q10: Top 3 Products per Category
SELECT * FROM (
    SELECT product_name, category_id, list_price,
    ROW_NUMBER() OVER(PARTITION BY category_id ORDER BY list_price DESC) as r
    FROM production.products
) t WHERE r <= 3;


-- Q13: Brands Pivot
SELECT * FROM (SELECT category_id, brand_name, product_id FROM production.products) t
PIVOT (COUNT(product_id) FOR brand_name IN ([Electra], [Haro], [Trek], [Surly])) p;


-- Q18 & Q20: Retention
SELECT customer_id FROM sales.orders WHERE YEAR(order_date) = 2017
INTERSECT
SELECT customer_id FROM sales.orders WHERE YEAR(order_date) = 2018;

-- Lost Customers
SELECT customer_id FROM sales.orders WHERE YEAR(order_date) = 2016
EXCEPT
SELECT customer_id FROM sales.orders WHERE YEAR(order_date) = 2017;