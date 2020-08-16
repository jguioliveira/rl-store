CREATE DEFINER=`root`@`localhost` PROCEDURE `order_insert`(
	
	IN varId varchar(32),
    IN varCustomerId varchar(32),
    IN varStatus tinyint,
    IN varTotal decimal(14,2),
    IN varCreated date,
    IN varUpdated date,
    IN varPaymentForm tinyint
)
BEGIN
	insert into TB_Order (Id, CustomerId, Status, Total, Created, Updated, PaymentForm)
    values (varId, varCustomerId, varStatus, varTotal, varCreated, varUpdated, varPaymentForm);
END