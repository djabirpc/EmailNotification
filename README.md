# EmailNotification
Develop system of Email notification if contract is almost to finish

## Package
we used HangFire Package  
```
<PackageReference Include="Hangfire" Version="1.8.6" />
<PackageReference Include="Hangfire.AspNetCore" Version="1.8.6" />
<PackageReference Include="Hangfire.SqlServer" Version="1.8.6" />
```  
## Daily program that automate
```
RecurringJob.AddOrUpdate<IContractService>("DailyTasl", x => x.CheckAndSendContractNotifications(), Cron.Daily());  
```



