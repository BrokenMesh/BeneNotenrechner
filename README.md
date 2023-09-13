<a name="readme-top"></a>

<div align="center">
<h3 align="center">BeneNotenrechner</h3>

  <p align="center">
    BeneNotenrechner is a school project. Its a web-based application developed using ASP.NET, React, and MySQL. This application enables students to conveniently monitor their school grades. Student data is securely stored through an account system and encryption measures.
    <br />
    <a href="https://github.com/BrokenMesh/BeneNotenrechner/issues">Report Bug</a>
    ·
    <a href="https://github.com/BrokenMesh/BeneNotenrechner/issues">Request Feature</a>
  </p>
</div>


<!-- BUILD -->
## Build Project

1. **Clone the Repository**
   
   To get started, clone the repository using the following command:
   ```sh
   git clone https://github.com/BrokenMesh/BeneNotenrechner.git
   ```
   
2. **Install the Latest MySQL Version**
   
   You can download and install the latest version of MySQL from [this link](https://dev.mysql.com/downloads/installer).
  
3. **Import the Database**

   Import the database by using the 'BeneNotenRechner.sql' file found in the root directory.
   
4. **Build the Project with Visual Studio**

   Building the project with Visual Studio can be done in various ways. You can either build or publish both the 'BeneNotenrechner' and 'Dev_API' projects.

5. **Configuration**

   Upon startup, the application will search for a 'Config.txt' file in the working directory. If it doesn't find one, it will use the default settings:
   - Host: "localhost"
   - User: "root"
   - Password: ""
   - Database: "benenotenrechner_db"
   - Mail Server: "http://localhost:5008/api/TokenMail"
   - Required Host: ""
   
   If you need different configurations, you should copy the 'Config.txt' file from the 'BeneNotenrechner' project to the working directory of the final executable and make the necessary changes.

6. **Mail Server Configuration**
   The BeneNotentool utilizes a separate executable, known as Dev_API (the name may change), to handle emails. Configuration for this is also managed through a 'Config.txt' file located in the project's working directory.

<!-- LICENSE -->
## License

Distributed under the GNU GENERAL PUBLIC LICENSE License. See `LICENSE.txt` for more information.

<!-- CONTACT -->
## Contact

Hicham El-Kord - elkordhicham@gmail.com

Project Link: [https://github.com/BrokenMesh/BeneNotenrechner](https://github.com/github_username/repo_name)


<p align="right">(<a href="#readme-top">back to top</a>)</p>
