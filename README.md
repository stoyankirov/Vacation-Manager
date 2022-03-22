# Vacation-Manager

### Installation

1. Clone the repository.  
   ```sh
   git clone https://github.com/stoyankirov/Vacation-Manager.git
   ```
2. Set up database.

	<font size="3">2.1. Open Microsoft SQL Server Management Studio.</font>  
	<font size="3">2.2. Right click on **Databases** and select **Restore Database**.</font>  
	<font size="3">2.3. Navigate to **DatabaseBackup** folder under cloned repository directory and restore it.</font>

3. Set up server.  
	<font size="3">3.1. Open cloned folder and run the following command:</font>
   ```sh
   cd Server\VacationManager\VacationManager.API
   ```

   <font size="3">3.2.  Allow user-secrets: </font>
	  ```sh
   dotnet user-secrets init
   ```
   <font size="3">3.3.  Set up the neccessery secrets: </font>
   
	```sh
   dotnet user-secrets set "Secrets:ConnectionString" "server={ServerInstance};database=VacationManager;Trusted_Connection=true;MultipleActiveResultSets=true;"
   ```
	***Important:*** Replace {ServerInstance} with your local MSSQL Server instance name.

   <font size="3">3.4. Run the server:</font>
   ```sh
   dotnet run
   ```

![image](https://user-images.githubusercontent.com/51677980/158018953-b2767260-6b8d-4404-aa3c-2aa523590e8d.png)

