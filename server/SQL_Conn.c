//
// Created by tania on 30/10/20.
//

#include "SQL_Conn.h"

#include <string.h>
#include <unistd.h>
#include <stdlib.h>
#include <sys/socket.h>
#include <netinet/in.h>
#include <stdio.h>
#include "mysql.h"


/***
 * starts a MySQL connection
 * @param conn MySQL connection
 * @param host host of the connection
 * @param user user of the connection
 * @param passw password of the connection
 * @param db database to connect to
 * @return -1 if any error happens, 0 elsewise
 */
int initMySQLServer(MYSQL **conn,char* host, char* user, char* passw, char* db) {
    //Creamos una conexion al servidor MYSQL
    *conn = mysql_init(NULL);
    if (conn == NULL) {
        printf("Error al crear la conexion: %u %s\n",
               mysql_errno(*conn), mysql_error(*conn));
        return -1;
    }

    //inicializar la conexion, indicando nuestras claves de acceso
    // al servidor de bases de datos (userName,pass)
    *conn = mysql_real_connect(*conn, host, user, passw, db, 0, NULL,
                               0); // CHANGE TO THE COMPUTER SETTINGS
    if (*conn == NULL) {
        printf("Error al inicializar la conexion: %u %s\n",
               mysql_errno(*conn), mysql_error(*conn));
        return -1;
    }
    return 0;
}

/***
 * Checks if a user exists and log him in if the password is correct.
 * @param conn MySQL connection
 * @param username username of the user
 * @param password password of the user
 * @return 0 if the user is able to login -1 if any error happens or if the login is incorrect
 */
int loginUser(MYSQL *conn, char username[20], char password[20]) {
    char consult[120] = {};
    MYSQL_RES *result;
    MYSQL_ROW Pwd;
    if (sprintf(consult, "SELECT Pwd FROM Users WHERE (Username = \'%s\');", username) < 0) {
        return -1;
    } else {
        if (mysql_query(conn, consult) < 0) {
            return -1;
        }
    }
    result = mysql_store_result(conn);
    if (result == NULL)return -1;
    Pwd = mysql_fetch_row(result);
    if (Pwd == NULL)return -1;
    if (strcmp(Pwd[0], password) == 0) return 0;
    return -1;
}



/***
 * registers a user in the database
 * @param conn MySQL connection
 * @param username new user's username
 * @param password new user's password
 * @param age new user's age
 * @param mail new user's mail
 * @param spam new user's spamvalue
 * @return -1 if any error happens, 0 if there are no errors
 */
int addUser(MYSQL *conn, char username[20], char password[20], int age, char mail[20], int spam) {
    char consult[120] = {};
    MYSQL_RES *result;
    MYSQL_ROW row;
    if (sprintf(consult, "SELECT * FROM Users WHERE (Username = \'%s\');", username) < 0) {
        return -1;
    } else {
        if (mysql_query(conn, consult) < 0) {
            return -1;
        }
    }
    result = mysql_store_result(conn);
    row = mysql_fetch_row(result);
    if (row != NULL)return -1;
    if (sprintf(consult, "INSERT INTO Users(Username,Pwd,Age, Mail,Spam) VALUES (\'%s\',\'%s\',\'%d\',\'%s\',\'%d\');",
                username, password, age, mail, spam) < 0) {
        return -1;
    } else {
        if (mysql_query(conn, consult) < 0) {
            return -1;
        }
    }
    return 0;


}

int deleteUser(struct MYSQL *conn, char username[20]){
    char consult1[120] = {};
    char consult2[120] = {};
    if (sprintf(consult1, "DELETE  FROM  UsersPerGame WHERE (UserId = (SELECT Id FROM Users WHERE Username = \'%s\'));", username) < 0) {
        return -1;
    } else {
        if (mysql_query(conn, consult1) < 0) {
            return -1;
        }
    }
    if (sprintf(consult2, "DELETE FROM Users WHERE (Username = \'%s\');", username) < 0) {
        return -1;
    } else {
        if (mysql_query(conn, consult2) < 0) {
            return -1;
        }
    }

    return 0;
}

/***
 * get's the email of a user from the database
 * @param conn MySQL connection
 * @param username username of the user
 * @param mailOutput output where the mail is copied
 * @return -1 if any error happens, 0 if there are no errors
 */
int getEmail(MYSQL *conn, char username[20], char mailOutput[20]) {
    char consult[120] = {};
    MYSQL_RES *result;
    MYSQL_ROW Mail;
    if (sprintf(consult, "SELECT Mail FROM Users WHERE (Username = \'%s\');", username) < 0) {
        return -1;
    } else {
        if (mysql_query(conn, consult) < 0) {
            return -1;
        }
    }
    result = mysql_store_result(conn);
    Mail = mysql_fetch_row(result);
    if (Mail == NULL)return -1;
    strcpy(mailOutput, Mail[0]);
    return 0;
}

/***
 * changes the value of spam to a new one
 * @param conn MySQL connection
 * @param username username of the user
 * @param spam new spam value
 * @return -1 if any error happens, 0 if there are no errors
 */
int setSpam(MYSQL *conn, char username[20], int spam) {
    char consult[120] = {};
    if (sprintf(consult, "Update Users SET spam=\'%d\' WHERE (Username = \'%s\');", spam, username) < 0) {
        return -1;
    } else {
        if (mysql_query(conn, consult) < 0) {
            return -1;
        }
    }
    return 0;
}

/**
 * get the current spam of a user
 * @param conn MySQL connection
 * @param username username of the user
 * @param spam value where the current spam is set
 * @return -1 if any error happens, 0 if there are no errors
 */
int getSpam(MYSQL *conn, char username[20], int *spam) {
    char consult[120] = {};
    MYSQL_RES *result;
    MYSQL_ROW SpamDB;
    if (sprintf(consult, "SELECT Spam FROM Users WHERE (Username = \'%s\');", username) < 0) {
        return -1;
    } else {
        if (mysql_query(conn, consult) < 0) {
            return -1;
        }
    }
    result = mysql_store_result(conn);
    SpamDB = mysql_fetch_row(result);
    if (SpamDB == NULL)return -1;
    *spam = (int) strtol(SpamDB[0], (char **) NULL, 10);
    return 0;
}

/**
 * get the age of a user from the database
 * @param conn MySQL connection
 * @param username username of the user
 * @param ageOutput vale where the current age is set
 * @return -1 if any error happens, 0 if there are no errors
 */
int getAge(MYSQL *conn, char username[20], int *ageOutput) {
    char consult[120] = {};
    MYSQL_RES *result;
    MYSQL_ROW Age;
    if (sprintf(consult, "SELECT Age FROM Users WHERE (Username = \'%s\');", username) < 0) {
        return -1;
    } else {
        if (mysql_query(conn, consult) < 0) {
            return -1;
        }
    }
    result = mysql_store_result(conn);
    Age = mysql_fetch_row(result);
    if (Age == NULL)return -1;
    *ageOutput = (int) strtol(Age[0], (char **) NULL, 10);
    return 0;
}


//GAME SQL COMMANDS
/**
 * function that creates a game in the database
 * @param conn MySQL connection
 * @param username username of the user
 * @param game value where the game id is set
 * @return -1 if any error happens, 0 if there are no errors
 */
int createGame(MYSQL* conn, char username[20], int *game){
    char consult[512] = {};
    MYSQL_RES *result;
    MYSQL_ROW GameRow;
    if (sprintf(consult, "INSERT INTO Games() VALUES ();") < 0) {
        return -1;
    } else {
        if (mysql_query(conn, consult) < 0) {
            return -1;
        }
    }
    if (sprintf(consult, "INSERT INTO UsersPerGame(gameid, userid) VALUES ((SELECT LAST_INSERT_ID()),(SELECT Id FROM Users WHERE Username=\'%s\'));",username) < 0) {
        return -1;
    } else {
        if (mysql_query(conn, consult) < 0) {
            return -1;
        }
    }
    if (sprintf(consult, "SELECT GameId FROM UsersPerGame, Users WHERE UserId=Users.Id AND Username=\'%s\';",username) < 0) {
        return -1;
    } else {
        if (mysql_query(conn, consult) < 0) {
            return -1;
        }
    }
    result = mysql_store_result(conn);
    GameRow = mysql_fetch_row(result);
    if (GameRow == NULL)return -1;
    *game = (int) strtol(GameRow[0], (char **) NULL, 10);
    return 0;
}

/**
 * set's in the database that a player joined a game
 * @param conn MySQL connection
 * @param username username of the user
 * @param game game to join
 * @return -1 if any error happens, 0 if there are no errors
 */
int joinGame(MYSQL* conn, char username[20], int game){
    char consult[120] = {};
    if (sprintf(consult, "INSERT INTO UsersPerGame(gameid, userid) VALUES (%d,(SELECT Id FROM Users WHERE Username=\'%s\'));", game, username) < 0) {
        return -1;
    } else {
        if (mysql_query(conn, consult) < 0) {
            return -1;
        }
    }
    return 0;
}
/**
 * starts a game saving it's players
 * @param conn MySQL connection
 * @param players list of players to set
 * @param game game to be started
 * @return -1 if any error happens, 0 if there are no errors
 */
int startGame(MYSQL* conn, char* players, int game){
    char consult[512] = {};
    if (sprintf(consult, "UPDATE Games SET Players=\'%s\' WHERE Games.Id=%d;",players, game) < 0) {
        return -1;
    } else {
        if (mysql_query(conn, consult) < 0) {
            return -1;
        }
    }
    return 0;
}

/**
 * deletes a player from a game
 * @param conn  MySQL connection
 * @param username username of the user
 * @param game game where the user is deleted
 * @return -1 if any error happens, 0 if there are no errors
 */
int exitGame(MYSQL* conn, char username[20], int game){
    char consult[120] = {};
    if (sprintf(consult, "DELETE FROM UsersPerGame WHERE (GameId = %d AND UserId =(SELECT Id FROM Users WHERE Username=\'%s\'));", game, username) < 0) {
        return -1;
    } else {
        if (mysql_query(conn, consult) < 0) {
            return -1;
        }
    }
    return 0;
}

/**
 * get's all the users of a game
 * @param conn MySQL connection
 * @param ans list where the players are dumped
 * @param userList original list
 * @param game game of the users
 * @return -1 if any error happens, 0 if there are no errors
 */
int usersFromGame(MYSQL* conn, UserList *ans, UserList *userList, int game){
    char consult[512] = {};
    MYSQL_RES *result;
    MYSQL_ROW userRow;
    if (sprintf(consult, "SELECT Username FROM UsersPerGame, Users WHERE UsersPerGame.UserId=Users.Id AND GameId=%d;",game) < 0) {
        return -1;
    } else {
        if (mysql_query(conn, consult) < 0) {
            return -1;
        }
    }
    result = mysql_store_result(conn);
    if(result==NULL)return -1;
    userRow = mysql_fetch_row(result);
    if (userRow == NULL)return -1;
    int i=0;
    while(userRow!=NULL){
        strcat((char *) ans->list[i].userName, userRow[0]);
        for(int j = 0; j<userList->num;j++)
        {
            if(strcmp(userList->list[j].userName, userRow[0])==0)
            {
                ans->list[i].socket =userList->list[j].socket;
                break;
            }
        }
        userRow = mysql_fetch_row(result);
        i++;
    }
    ans->num=i;
    return 0;
}

/**
 * gives a list of all the ongoing games separated by commas
 * @param conn  MySQL connection
 * @param games char array where the games are copied to
 * @return -1 if any error happens, 0 if there are no errors
 */
int ongoingGames(MYSQL* conn, char games[512]){
    char consult[512] = {};
    MYSQL_RES *result;
    MYSQL_ROW row;
    if (sprintf(consult, "SELECT GameId FROM UsersPerGame") < 0) {
        return -1;
    } else {
        if (mysql_query(conn, consult) < 0) {
            return -1;
        }
    }
    result = mysql_store_result(conn);
    row = mysql_fetch_row(result);
    if (row == NULL)return -1;

    while(row != NULL){

        strcat(games,row[0]);
        strcat(games,",");

        row = mysql_fetch_row(result);
    }
    return 0;

}

/**
 * gives a string with all the finisihed games, it's players and their scores
 * following the next formula id-player1*player2*-score,id-player2*player3*player4*-score,...
 * @param conn
 * @param games
 * @return
 */
int finishedGames(MYSQL* conn, char games[512]){
    char consult[512] = {};
    MYSQL_RES *result;
    MYSQL_ROW row;
    if (sprintf(consult, "SELECT DISTINCT Games.Id,Games.Players,Games.Score FROM Games WHERE Games.Id NOT IN (SELECT GameId FROM UsersPerGame) ORDER BY Games.Score DESC;") < 0) {
        return -1;
    } else {
        if (mysql_query(conn, consult) < 0) {
            return -1;
        }
    }
    result = mysql_store_result(conn);
    row = mysql_fetch_row(result);
    if (row == NULL)return -1;

    while(row != NULL){

        if(row[1]==NULL){
            return -1;
        }
        strcat(games,row[0]);
        strcat(games,"-");
        strcat(games,row[1]);
        strcat(games,"-");
        strcat(games,row[2]);
        strcat(games,"-,");
        row = mysql_fetch_row(result);
    }
    return 0;
}

/**
 * cleans a table of the database
 * @param conn MySQL connection
 * @param table tablename to trucate
 * @return -1 if any error happens, 0 if there are no errors
 */
int resetSQLTable(MYSQL *conn, char table[20]) {
    char consult[512]={};
    if (sprintf(consult, "TRUNCATE %s;",table) < 0) {
        return -1;
    } else {
        if (mysql_query(conn, consult) < 0) {
            return -1;
        }
    }
    return 0;
}


