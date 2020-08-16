CREATE PROCEDURE orderItem_insert (
	
    IN varOrderId varchar(32),
    IN varProductId varchar(32),
    IN varCount integer,
    IN varUnitValue decimal(14,2),
    IN varTotal decimal(14,2),
    IN varProductName varchar(100)
)
BEGIN
	insert into TB_OrderItem (OrderId, ProductId, Count, UnitValue, Total, ProductName)
    values (varOrderItem, varProductId, varCount, varUnitValue, varTotal, varProductName);
END