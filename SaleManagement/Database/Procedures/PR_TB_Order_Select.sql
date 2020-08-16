CREATE DEFINER=`root`@`localhost` PROCEDURE `order_select`(
	
    IN inicialDate date,
    IN finalDate date
)
BEGIN
	select Id, CustomerId, Status, Total, Created, Updated, PaymentForm from tb_order where Created between inicialDate and finalDate;
END