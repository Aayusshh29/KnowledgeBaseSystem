using Backend.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.Data;
using Backend.Models;

namespace Backend.Controllers
{
    // Endpoint: http://localhost:5041/api/dashboard
    [ApiController]
    [Route("api/[controller]")]
    public class DashboardController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IPolicyService _policyService;
        private readonly IProcedureService _procedureService;
        private readonly IFAQService _faqService;
        private readonly IPolicyRequestService _policyRequestService;
        private readonly ApplicationDbContext _context;

        public DashboardController(
            IUserService userService,
            IPolicyService policyService,
            IProcedureService procedureService,
            IFAQService faqService,
            IPolicyRequestService policyRequestService,
            ApplicationDbContext context)
        {
            _userService = userService;
            _policyService = policyService;
            _procedureService = procedureService;
            _faqService = faqService;
            _policyRequestService = policyRequestService;
            _context = context;
        }

        // GET: api/dashboard/stats
        [HttpGet("stats")]
        public async Task<IActionResult> GetStatistics()
        {
            // Get counts
            var users = await _userService.GetAllUsersAsync();
            var policies = await _policyService.GetAllPoliciesAsync();
            var procedures = await _procedureService.GetAllProceduresAsync();
            var faqs = await _faqService.GetAllFAQsAsync();
            var policyRequests = await _policyRequestService.GetAllPolicyRequestsAsync();

            // Get the latest policy request
            var latestPolicyRequest = await _context.PolicyRequests
                .OrderByDescending(pr => pr.Id)
                .Select(pr => new
                {
                    Type = "PolicyRequest",
                    Title = pr.Title,
                    Timestamp = pr.RequestedAt,
                    Message = "New policy request submitted"
                })
                .FirstOrDefaultAsync();

            // Get the latest policy creation
            var latestPolicy = await _context.Policies
                .OrderByDescending(p => p.Id)
                .Select(p => new
                {
                    Type = "PolicyCreation",
                    Title = p.Title,
                    Timestamp = !string.IsNullOrEmpty(p.CreatedAt) ? p.CreatedAt : null,
                    Message = "New policy added: " + p.Title
                })
                .FirstOrDefaultAsync();

            // Get the latest updated policy
            var latestPolicyUpdate = await _context.Policies
                .Where(p => p.LastUpdatedAt != null)
                .OrderByDescending(p => p.Id)
                .Select(p => new
                {
                    Type = "PolicyUpdate",
                    Title = p.Title,
                    Timestamp = p.LastUpdatedAt,
                    Message = p.Title + " policy updated"
                })
                .FirstOrDefaultAsync();

            // Get the latest procedure creation
            var latestProcedure = await _context.Procedures
                .OrderByDescending(p => p.Id)
                .Select(p => new
                {
                    Type = "ProcedureCreation",
                    Title = p.Title,
                    Timestamp = !string.IsNullOrEmpty(p.CreatedAt) ? p.CreatedAt : null,
                    Message = "New procedure added: " + p.Title
                })
                .FirstOrDefaultAsync();

            // Get the latest user - without timestamp since it doesn't exist
            var latestUser = await _context.Users
                .OrderByDescending(u => u.Id)
                .Select(u => new
                {
                    Type = "UserRegistration",
                    Name = u.Name,
                    Message = "New user registered: " + u.Name
                })
                .FirstOrDefaultAsync();

            // Get the latest FAQ - without timestamp since it doesn't exist
            var latestFaq = await _context.FAQs
                .OrderByDescending(f => f.Id)
                .Select(f => new
                {
                    Type = "FAQ",
                    Question = f.Question,
                    Message = "New FAQ added: " + f.Question
                })
                .FirstOrDefaultAsync();

            // Create list of recent activities
            var recentActivities = new List<object>();
            if (latestPolicyRequest != null) recentActivities.Add(latestPolicyRequest);
            if (latestPolicy != null) recentActivities.Add(latestPolicy);
            if (latestPolicyUpdate != null) recentActivities.Add(latestPolicyUpdate);
            if (latestProcedure != null) recentActivities.Add(latestProcedure);
            if (latestUser != null) recentActivities.Add(latestUser);
            if (latestFaq != null) recentActivities.Add(latestFaq);

            var stats = new
            {
                TotalUsers = users.Count(),
                TotalPolicies = policies.Count(),
                TotalProcedures = procedures.Count(),
                TotalFAQs = faqs.Count(),
                TotalPolicyRequests = policyRequests.Count(),
                RecentActivities = recentActivities
            };

            return Ok(stats);
        }
    }
}