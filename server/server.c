#include <string.h>
#include <unistd.h>
#include <stdlib.h>
#include <sys/types.h>
#include <sys/socket.h>
#include <netinet/in.h>
#include <stdio.h>
#include <mysql.h>


int initMySQLServer(MYSQL *conn) {
	int err;
	//Creamos una conexion al servidor MYSQL
	conn = mysql_init(NULL);
	if (conn == NULL)
	{
		printf("Error al crear la conexion: %u %s\n",
			mysql_errno(conn), mysql_error(conn));
		return -1;
	}

	//inicializar la conexion, indicando nuestras claves de acceso
	// al servidor de bases de datos (user,pass)
	conn = mysql_real_connect(conn, "localhost", "root", "mysql", "GameDB", 0, NULL, 0); // CHANGE TO THE COMPUTER SETTINGS 
	if (conn == NULL)
	{
		printf("Error al inicializar la conexion: %u %s\n",
			mysql_errno(conn), mysql_error(conn));
		return -1;
	}
	return 0;
}

int startTCPServer(struct sockaddr_in serv_adr, int * sock_conn, int * sock_listen, int PORT) {
	if ((sock_listen = socket(AF_INET, SOCK_STREAM, 0)) < 0) {
		printf("Error creating socket");
		return -1;
	}
	printf("Socket created");


	memset(&serv_adr, 0, sizeof(serv_adr));// inicialitza a zero serv_addr
	serv_adr.sin_family = AF_INET;

	// asocia el socket a cualquiera de las IP de la m?quina. 
	//htonl formatea el numero que recibe al formato necesario
	auto a=htonl(INADDR_ANY);
	//serv_adr.sin_addr.s_addr = htonl(INADDR_ANY);
	serv_adr.sin_addr.s_addr = inet_addr("10.0.3.15");
	
	// establecemos el puerto de escucha
	serv_adr.sin_port = htons(PORT);
	if (bind(sock_listen, (struct sockaddr*)&serv_adr, sizeof(serv_adr)) != 0) {
		printf("Error binding");
		return -1;
	}
	printf("binding ended");

	if (listen(sock_listen, 3) != 0) {
		printf("Error en el Listen");
		return -1;
	}
	printf("listening");
	sock_conn = accept(sock_listen, NULL, NULL);
	printf("accepted at socket: %d",sock_conn);
	return 0;
}

int loginUser(MYSQL* conn, char username[20], char password[20]) {
	char consult[120] = {};
	int err;
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
	if (err != 0) {
		printf("Error user not found %u %s\n",
			mysql_errno(conn), mysql_error(conn));
		return -1;
	}
	result = mysql_store_result(conn);
	Pwd = mysql_fetch_row(result);
	if (Pwd == NULL)return -1;
	if (strcmp(Pwd[0], password) == 0) return 0;
	return -1;
}

int addUser(MYSQL *conn, char username[20], char password[20], int age, char mail[20], int spam){
	char consult[120]={};
	if(sprintf(consult,"INSERT INTO Users(Username,Pwd,Age, Mail,Spam) VALUES (\'%s\',\'%s,\'%d\',\'%s\',\'%d\');",username, password, age, mail,!spam)<0){
		return -1;
	}
	else {
		if(mysql_query(conn, consult)<0){
			return -1;
		}
	}
	return 0;
}

int getEmail(MYSQL* conn, char username[20], char mailOutput[30]) {
	char consult[120] = {};
	int err;
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
	if (err != 0) {
		printf("Error user not found %u %s\n",
			mysql_errno(conn), mysql_error(conn));
		return -1;
	}
	result = mysql_store_result(conn);
	Mail = mysql_fetch_row(result);
	if (Mail == NULL)return -1;
	mailOutput = Mail[0];
	return 0;
}

int setSpam(MYSQL* conn, char username[20], int spam) {
	char consult[120] = {};
	int err;
	if (sprintf(consult, "Update Users SET spam=\'%d\' WHERE (Username = \'%s\');", !spam, username) < 0) {
		return -1;
	}
	else {
		if (mysql_query(conn, consult) < 0) {
			return -1;
		}
	}
	if (err != 0) {
		printf("Error user not found %u %s\n",
			mysql_errno(conn), mysql_error(conn));
		return -1;
	}
	return 0;
}

int getAge(MYSQL* conn, char username[20], int* ageOutput) {
	char consult[120] = {};
	int err;
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
	if (err != 0) {
		printf("Error user not found %u %s\n",
			mysql_errno(conn), mysql_error(conn));
		return -1;
	}
	result = mysql_store_result(conn);
	Age = mysql_fetch_row(result);
	if (Age == NULL)return -1;
	ageOutput = atoi(Age[0]);
	return 0;
}


int main(int argc, char *argv[])
{
	int sock_conn, sock_listen, ret;
	struct sockaddr_in serv_adr;
	char petition[512]={};
	char answer[512]={};
	MYSQL *conn;
	
	char user[20] = {};
	char password[20] = {};
	int age=33;
	char mail[20] = {};
	int spam;
	
	if (initMySQLServer(conn) < 0) {
		printf("Error opening Mysql");
		exit(1);
	}

	if (startTCPServer(serv_adr,sock_conn, sock_listen, 13550) < 0) {
		printf("Error opening tcp socket");
		exit(1);
	}
	for (;;){
		printf ("Escuchando\n");
		
		
		printf ("connected\n");
		
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
				int code =  atoi (p);
				switch (code)
				{
				case 0:
					stop = 1;
					break;
				case 1:
					//LOGIN 0/user,password -->0/0 good 0/-1 NG
					p = strtok(data, ",");
					strcpy(user, p);
					p = strtok(NULL, ",");
					strcpy(password, p);
					sprintf(answer, "1/%d",loginUser(conn,user,password));
					break;
				case 2:
					//REGISTER 1/user,password,age,mail,spam -->1/0 good 1/-1 not good
					p = strtok(data, ",");
					strcpy(user, p);
					p = strtok(NULL, ",");
					strcpy(password, p);
					p = strtok(NULL, ",");
					age = atoi(p);
					p = strtok(NULL, ",");
					strcpy(mail, p);
					p = strtok(NULL, ",");
					spam = atoi(p);
					sprintf(answer, "2/%d", addUser(conn, user, password,age,mail,spam));
					break;
				case 3:
					//GET AGE 2/user -->2/age
					p = strtok(data, ",");
					strcpy(user, p);
					getAge(conn, user, &age);
					sprintf(answer, "3/%d",age);
					break;

				case 4:
					//GET MAIL 3/user -->3/mail
					p = strtok(data, ",");
					strcpy(user, p);
					getEmail(conn, user, mail);
					sprintf(answer, "4/%s", mail);
					break;
				case 5:
					//CHANGE SPAM 4/user,newspam -->4/0 changed 4/-1 not changed
					p = strtok(data, ",");
					strcpy(user, p);
					p = strtok(NULL, ",");
					spam = atoi(p);
					sprintf(answer, "5/%d",setSpam(conn, user, spam));
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
		}
		// Server end
		close(sock_conn); 
	}
	return 0;
}
