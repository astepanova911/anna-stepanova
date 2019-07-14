1.	Run BestBuy on localhost:3030
2.	Install VS Community 2017
3.	Open Solution
4.	Restore packages and build
5.	If missing libraries select Manage Nuget packages and check following packages are installed:
•	IdentityModel 1.12.0
•	Newtonsoft.Json 12.0.1
•	NJunut 3.10.1
•	RestSharp 106.6.10
•	System.Net.HTTP 4.3.4
6.	Check packages are in the References with correct versions
7.	After build is successful open Test -> Windows-> Test Explorer and execute the tests loaded
All tests are located in \BestBuy.API.Tests\TestCollection\BestBuyAPITests.cs 
