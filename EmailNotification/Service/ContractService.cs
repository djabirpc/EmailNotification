using System;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;
using EmailNotification.Service;
using EmailNotification.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Identity.UI.Services;

public class ContractService : IContractService
{
    private readonly IConfiguration _configuration;
    private readonly ILogger<ContractService> _logger;
    private readonly IEmailSender _emailSender;

    public ContractService(
        IConfiguration configuration,
        ILogger<ContractService> logger,
        IEmailSender emailSender)
    {
        _configuration = configuration;
        _logger = logger;
        _emailSender = emailSender;
    }

    public async Task CheckAndSendContractNotifications()
    {
        try
        {
            // Get contracts close to their end date (e.g., ending in the next 30 days) from your database
            var nearEndContracts = GetContractsNearEndDate();

            foreach (var contract in nearEndContracts)
            {
                string message = $"The contract {contract.ContractId} is almost finished";
                await _emailSender.SendEmailAsync(contract.ClientEmail, "Test From ASP.NET", message);

                // Log the notification
                _logger.LogInformation($"Email notification sent for contract ID: {contract.ContractId}");
            }
        }
        catch (Exception ex)
        {
            // Handle exceptions and log errors
            _logger.LogError($"Error in CheckAndSendContractNotifications: {ex.Message}");
        }
    }

    private List<Contract> GetContractsNearEndDate()
    {
        // Implement the logic to query your database and retrieve contracts close to their end date.
        // You might use Entity Framework or any other data access method.

        // For this example, we assume a simple list of contracts.
        var currentDate = DateTime.Now;
        var contracts = new List<Contract>
        {
            new Contract { ContractId = 1, ClientEmail = "djabir.perso@gmail.com", EndDate = currentDate.AddDays(10) },
            new Contract { ContractId = 2, ClientEmail = "kahloucheomardjaber@gmail.com", EndDate = currentDate.AddDays(20) }
        };

        //return contracts.Where(c => (c.EndDate - currentDate).Days <= 5).ToList();
        return contracts;
    }
}
