-- تحسين البحث بـ Email و Category/Brand و تقارير التاريخ
CREATE INDEX IX_Email ON sales.customers(email);
CREATE INDEX IX_CatBrand ON production.products(category_id, brand_id);
CREATE INDEX IX_OrderDate ON sales.orders(order_date) INCLUDE(customer_id, store_id,CREATE TABLE sales.customer_log (log_id INT IDENTITY PRIMARY KEY, customer_id INT, action VARCHAR(50), log_date DATETIME DEFAULT GETDATE());
CREATE TABLE production.price_history (history_id INT IDENTITY PRIMARY KEY, product_id INT, old_price DECIMAL, new_price DECIMAL, change_date DATETIME DEFAULT GETDATE(), changed_by VARCHAR(50));
CREATE TABLE sales.order_audit (audit_id INT IDENTITY PRIMARY KEY, order_id INT, customer_id INT, store_id INT, staff_id INT, order_date DATE, audit_timestamp DATETIME DEFAULT GETDATE()); order_status);

-- 4. ترحيب بالعميل
CREATE TRIGGER trg_1 ON sales.customers AFTER INSERT AS 
INSERT INTO sales.customer_log(customer_id, action) SELECT customer_id, 'Welcome' FROM inserted;

-- 5. سجل تاريخ الأسعار
CREATE TRIGGER trg_2 ON production.products AFTER UPDATE AS IF UPDATE(list_price)
INSERT INTO production.price_history(product_id, old_price, new_price, changed_by)
SELECT d.product_id, d.list_price, i.list_price, USER_NAME() FROM deleted d JOIN inserted i ON d.product_id=i.product_id;

-- 6. منع الحذف إذا وجد منتجات مرتبط
CREATE TRIGGER trg_3 ON production.categories INSTEAD OF DELETE AS
IF EXISTS(SELECT 1 FROM production.products WHERE category_id IN (SELECT category_id FROM deleted))
    THROW 50000, 'Cannot delete category with products.', 1;
ELSE DELETE FROM production.categories WHERE category_id IN (SELECT category_id FROM deleted);

-- 7. خصم الكمية من المخزن تلقائياً
CREATE TRIGGER trg_4 ON sales.order_items AFTER INSERT AS
UPDATE s SET s.quantity -= i.quantity FROM production.stocks s JOIN inserted i ON s.product_id=i.product_id 
JOIN sales.orders o ON i.order_id=o.order_id WHERE s.store_id=o.store_id;

-- 8. مراقبة الطلبات الجديدة
CREATE TRIGGER trg_5 ON sales.orders AFTER INSERT AS
INSERT INTO sales.order_audit(order_id, customer_id, store_id, staff_id, order_date) 
SELECT order_id, customer_id, store_id, staff_id, order_date FROM inserted;
