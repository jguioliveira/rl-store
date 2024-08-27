CREATE DEFINER=`root`@`localhost` PROCEDURE `PR_TB_OrderItem_Id_Select`(
	
    IN varId varchar(32)
)
BEGIN
	select oi.OrderId, oi.ProductId, oi.Count, oi.UnitValue, oi.Total, oi.ProductName
    from tb_order as o inner join tb_orderitem as oi on o.Id = oi.OrderId
    where oi.OrderId = varId;
END