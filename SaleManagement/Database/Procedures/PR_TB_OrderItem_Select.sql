CREATE PROCEDURE order_item_select (
	
    IN initialDate date,
    IN finalDate date
)
BEGIN
	select oi.OrderId, oi.ProductId, oi.Count, oi.UnitValue, oi.Total, oi.ProductName
    from tb_order as o inner join tb_orderitem as oi on o.Id = oi.OrderId
    where o.Created between initialDate and finalDate;
END
