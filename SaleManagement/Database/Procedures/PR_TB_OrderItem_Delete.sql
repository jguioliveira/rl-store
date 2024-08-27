CREATE DEFINER=`root`@`localhost` PROCEDURE `PR_TB_OrderItem_Delete`(
	IN varProductId varchar(32)
)
BEGIN
	Delete from tb_orderitem where ProductId = varProductId;
END