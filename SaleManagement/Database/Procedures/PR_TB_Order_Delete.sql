CREATE DEFINER=`root`@`localhost` PROCEDURE `PR_TB_Order_Delete`(
	IN varId varchar(32)
)
BEGIN
	Delete from tb_orderitem where OrderId = varId;
    Delete from tb_order where Id = varId;
END