create database RLSalesDB;
create table TB_Order(
	Id varchar(32)not null,
    Custmomerid varchar(32),
    Status tinyint,
    Total decimal(14,2)not null,
    Created date not null,
    Updated date not null,
    PaymentForm tinyint,
    primary key(Id)
);

create table TB_OrderItem(
	OrderId varchar(32),
    ProductId varchar(32),
    Count integer,
    UnitValue decimal(14,2)not null,
    Total decimal(14,2)not null,
    ProductName varchar(100),
    primary key(OrderId, ProductId)
    
);

alter table TB_OrderItem add constraint fk_order foreign key (OrderId) references TB_Order(Id);
describe TB_OrderItem;tb_orderitem