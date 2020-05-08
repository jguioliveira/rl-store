db.createUser(
    {
        user: "adminuser",
        pwd: "qwerty11",
        roles: [
            {
                role: "readWrite",
                db: "UserstoreDb"
            }
        ]
    }
);