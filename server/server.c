#include <string.h>
#include <unistd.h>
#include <stdlib.h>
#include <sys/socket.h>
#include <netinet/in.h>
#include <stdio.h>
#include "mysql.h"


int initMySQLServer(MYSQL **conn) {
	//Creamos una conexion al servidor MYSQL
	*conn = mysql_init(NULL);
	if (conn == NULL)
	{
		printf("Error al crear la conexion: %u %s\n",
			mysql_errno(*conn), mysql_error(*conn));
		return -1;
	}

	//inicializar la conexion, indicando nuestras claves de acceso
	// al servidor de bases de datos (user,pass)
	*conn = mysql_real_connect(*conn, "localhost", "root", "", "GameDB", 0, NULL, 0); // CHANGE TO THE COMPUTER SETTINGS
	if (*conn == NULL)
	{
		printf("Error al inicializar la conexion: %u %s\n",
			mysql_errno(*conn), mysql_error(*conn));
		return -1;
	}
	return 0;
}

int startTCPServer(struct sockaddr_in *serv_adr, int * sock_conn, int * sock_listen, int PORT) {
	if ((*sock_listen = socket(AF_INET, SOCK_STREAM, 0)) < 0) {
		printf("Error creating socket\n");
		return -1;
	}
	printf("Socket created\n");


	memset(serv_adr, 0, sizeof(*serv_adr));// inicialitza a zero serv_addr
	serv_adr->sin_family = AF_INET;

	// asocia el socket a cualquiera de las IP de la m?quina. 
	//htonl formatea el numero que recibe al formato necesario
	serv_adr->sin_addr.s_addr = htonl(INADDR_ANY);
	
	// establecemos el puerto de escucha
	serv_adr->sin_port = htons(PORT);
	if (bind(*sock_listen, (struct sockaddr *)serv_adr, sizeof(*serv_adr)) != 0) {
		printf("Error binding\n");
		return -1;
	}
	printf("binding ended\n");

	if (listen(*sock_listen, 3) != 0) {
		printf("Error en el Listen\n");
		return -1;
	}
	printf("listening\n");
	*sock_conn = accept(*sock_listen, NULL, NULL);
	printf("accepted at socket: %d\n",*sock_conn);
	return 0;
}

int loginUser(MYSQL *conn, char username[20], char password[20]) {
	char consult[120] = {};
	MYSQL_RES* result;
	MYSQL_ROW Pwd;
	if (sprintf(consult, "SELECT Pwd FROM Users WHERE (Username = \'%s\');", username) < 0) {
		return -1;
	}
	else {
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

int addUser(MYSQL *conn, char username[20], char password[20], int age, char mail[20], int spam){
	char consult[120]={};
	if(sprintf(consult,"INSERT INTO Users(Username,Pwd,Age, Mail,Spam) VALUES (\'%s\',\'%s\',\'%d\',\'%s\',\'%d\');",username, password, age, mail,!spam)<0){
		return -1;
	}
	else {
		if(mysql_query(conn, consult)<0){
			return -1;
		}
	}
	return 0;
}

int getEmail(MYSQL* conn, char username[20], char mailOutput[20]) {
	char consult[120] = {};
	MYSQL_RES* result;
	MYSQL_ROW Mail;
	if (sprintf(consult, "SELECT Mail FROM Users WHERE (Username = \'%s\');", username) < 0) {
		return -1;
	}
	else {
		if (mysql_query(conn, consult) < 0) {
			return -1;
		}
	}
	result = mysql_store_result(conn);
    Mail = mysql_fetch_row(result);
	if (Mail == NULL)return -1;
	strcpy(mailOutput,Mail[0]);
	return 0;
}

int setSpam(MYSQL* conn, char username[20], int spam) {
	char consult[120] = {};
	if (sprintf(consult, "Update Users SET spam=\'%d\' WHERE (Username = \'%s\');", !spam, username) < 0) {
		return -1;
	}
	else {
		if (mysql_query(conn, consult) < 0) {
			return -1;
		}
	}
	return 0;
}

int getSpam(MYSQL* conn, char username[20], int * spam){
    char consult[120] = {};
    MYSQL_RES* result;
    MYSQL_ROW SpamDB;
    if (sprintf(consult, "SELECT Spam FROM Users WHERE (Username = \'%s\');", username) < 0) {
        return -1;
    }
    else {
        if (mysql_query(conn, consult) < 0) {
            return -1;
        }
    }
    result = mysql_store_result(conn);
    SpamDB = mysql_fetch_row(result);
    if (SpamDB == NULL)return -1;
    *spam = (int)strtol(SpamDB[0],(char **) NULL,10);
    return 0;
}

int getAge(MYSQL* conn, char username[20], int* ageOutput) {
	char consult[120] = {};
	MYSQL_RES* result;
	MYSQL_ROW Age;
	if (sprintf(consult, "SELECT Age FROM Users WHERE (Username = \'%s\');", username) < 0) {
		return -1;
	}
	else {
		if (mysql_query(conn, consult) < 0) {
			return -1;
		}
	}
	result = mysql_store_result(conn);
	Age = mysql_fetch_row(result);
	if (Age == NULL)return -1;
	*ageOutput=(int)strtol(Age[0],(char **) NULL,10);
	return 0;
}


int main(int argc, char *argv[])
{
	int sock_conn, sock_listen, ret;
	struct sockaddr_in serv_adr;
	char petition[512]={};
	char answer[512]={};
	MYSQL *conn = NULL;
	
	char user[20] = {};
	char password[20] = {};
	int age=33;
	char mail[20] = {};
	int spam;

	if (initMySQLServer(&conn) < 0) {
		printf("Error opening Mysql");
		exit(1);
	}else{
        printf("Mysql opened");
	}

	if (startTCPServer(&serv_adr,&sock_conn, &sock_listen, 13555) < 0) {
		printf("Error opening tcp socket");
		exit(1);
	}
	else{
	    printf("connection secured");
	}


    int stop =0;


    while (stop ==0)
    {
        ret=read(sock_conn,petition, sizeof(petition));
        if(ret<0){
            printf("no data in buffer\n");
        }
        else{
            printf ("received\n");
            petition[ret]='\0';
            printf ("Petition: %s\n",petition);
            char *p = strtok( petition, "/");
            char data[512] = {};
            strcpy(data, p + 2);
            int code =  (int)strtol(p,(char **) NULL,10);
            switch (code)
            {
                case 0:
                    stop = 1;
                    break;
                case 1:
                    //LOGIN 1/user,password -->1/0 good 1/-1 NG
                    p = strtok(data, ",");
                    if(p!=NULL)strcpy(user, p);
                    p = strtok(NULL, ",");
                    if(p!=NULL)strcpy(password, p);
                    sprintf(answer, "1/%d",loginUser(conn,user,password));
                    break;
                case 2:
                    //REGISTER 1/user,password,age,mail,spam -->1/0 good 1/-1 not good
                    p = strtok(data, ",");
                    if(p!=NULL)strcpy(user, p);
                    p = strtok(NULL, ",");
                    if(p!=NULL)strcpy(password, p);
                    p = strtok(NULL, ",");
                    if(p!=NULL)age = (int)strtol(p,(char **) NULL,10);
                    p = strtok(NULL, ",");
                    if(p!=NULL)strcpy(mail, p);
                    p = strtok(NULL, ",");
                    if(p!=NULL)spam = (int)strtol(p,(char **) NULL,10);
                    sprintf(answer, "2/%d", addUser(conn, user, password,age,mail,spam));
                    break;
                case 3:
                    //GET AGE 2/user -->2/age
                    p = strtok(data, ",");
                    if(p!=NULL)strcpy(user, p);
                    getAge(conn, user, &age);
                    sprintf(answer, "3/%d",age);
                    break;

                case 4:
                    //GET MAIL 3/user -->3/mail
                    p = strtok(data, ",");
                    if(p!=NULL)strcpy(user, p);
                    getEmail(conn, user, mail);
                    sprintf(answer, "4/%s", mail);
                    break;
                case 5:
                    //CHANGE SPAM 4/user,newspam -->4/0 changed 4/-1 not changed
                    p = strtok(data, ",");
                    if(p!=NULL)strcpy(user, p);
                    p = strtok(NULL, ",");
                    if(p!=NULL)spam = (int)strtol(p,(char **) NULL,10);
                    sprintf(answer, "5/%d",setSpam(conn, user, spam));
                    break;
                case 6:
                    //GET SPAM 6/user-->6/1(true) else 6/0
                    p = strtok(data, ",");
                    if(p!=NULL)strcpy(user, p);
                    getSpam(conn, user, &spam);
                    sprintf(answer, "6/%d", spam);
                    break;
                default:
                    //ERROR CODE
                    sprintf(answer, "-1/%d", code);
            }
            if (code !=0)
            {
                printf ("Answer: %s\n", answer);
                // Send aswer
                write (sock_conn,answer, strlen(answer));
            }
        }
		// Server end
	}
    close(sock_conn);
    return 0;
}
