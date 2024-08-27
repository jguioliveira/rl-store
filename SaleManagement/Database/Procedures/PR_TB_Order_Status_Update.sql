CREATE DEFINER=`root`@`localhost` PROCEDURE `PR_TB_Order_Status_Update`(
	
    IN varId varchar(32),
    IN varStatus tinyint,
    IN varUpdated date
   
)
BEGIN
	Update tb_order SET Status = varStatus, Updated = varUpdated
    where Id = varId;
END