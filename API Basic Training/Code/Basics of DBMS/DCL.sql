-- DCL ( data control language )

-- create the new user
CREATE USER 	
	'prince.g' ;

-- grant the permission to specific user 
GRANT 
	SELECT,
    UPDATE,
    DELETE,      
    DELETE,      
    INSERT
ON 
	EMP01 
TO 
	'prince.g';
    
-- revoke the permission
REVOKE 
	UPDATE,
    DELETE,
    INSERT
ON 
	EMP01
FROM 
	'prince.g';