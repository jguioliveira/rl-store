CREATE DEFINER=`root`@`localhost` PROCEDURE `PR_TB_Order_Id_Select`(
	
    IN varId varchar(32)
)
BEGIN
	select Id, CustomerId, Status, Total, Created, Updated, PaymentForm from tb_order where Id = varId;
END