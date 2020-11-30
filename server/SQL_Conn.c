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

int usersFromGame(MYSQL* conn, UserList *ans, int game){
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
    userRow = mysql_fetch_row(result);
    if (userRow == NULL)return -1;
    int i=0;
    while(userRow!=NULL){
        strcat((char *) ans->list[i].userName, userRow[0]);
        userRow = mysql_fetch_row(result);
        i++;
    }
    ans->num=i;
    return 0;
}


