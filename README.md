# UserVerification
Azure Function to Verify Azure B2C with Azure Table 
This Azure Function is written with .Net core 3.X C# 9.0 
this Azure Function can read the Request body and compare User's Email Adress and Password with an existing User entity in Azure Table 
in this scenarion i'm using Azure B2C az an identity provider in order to authenticate the User and verify his credentials 
the response is given in JSON format or HTTP Status Code 
