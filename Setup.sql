use allspicebruh;

-- CREATE TABLE recipes (
--     id INT NOT NULL AUTO_INCREMENT,
--     name VARCHAR(255) NOT NULL,
--     description VARCHAR(255),

--     PRIMARY KEY (id)
-- );

CREATE TABLE ingredients (
    id INT NOT NULL AUTO_INCREMENT,
    name VARCHAR(255) NOT NULL, 
    amount VARCHAR(255) NOT NULL,
    recipeId INT NOT NULL,

    PRIMARY KEY (id),

    FOREIGN KEY (recipeId)
        REFERENCES recipes (id)
        ON DELETE CASCADE
);