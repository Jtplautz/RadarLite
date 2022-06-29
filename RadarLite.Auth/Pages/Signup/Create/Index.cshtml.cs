using System;
using System.Linq;
using System.Threading.Tasks;
using Duende.IdentityServer.Events;
using Duende.IdentityServer.Models;
using Duende.IdentityServer.Services;
using Duende.IdentityServer.Stores;
using IdentityServerHost.Models;
using IdentityServerHost.Pages;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace IdentityServerHost.Pages.Create;

[SecurityHeaders]
[AllowAnonymous]
public class Index : PageModel {
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly IIdentityServerInteractionService _interaction;
    private readonly IClientStore _clientStore;
    private readonly IEventService _events;
    private readonly IAuthenticationSchemeProvider _schemeProvider;
    private readonly IIdentityProviderStore _identityProviderStore;

    public ViewModel View { get; set; }

    [BindProperty]
    public InputModel Input { get; set; }

    public Index(
        IIdentityServerInteractionService interaction,
        IClientStore clientStore,
        IAuthenticationSchemeProvider schemeProvider,
        IIdentityProviderStore identityProviderStore,
        IEventService events,
        UserManager<ApplicationUser> userManager,
        SignInManager<ApplicationUser> signInManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _interaction = interaction;
        _clientStore = clientStore;
        _schemeProvider = schemeProvider;
        _identityProviderStore = identityProviderStore;
        _events = events;
    }

    public async Task<IActionResult> OnGet(string returnUrl)
    {///
        //await BuildModelAsync(returnUrl);

        return Page();
    }

    public async Task<IActionResult> OnPost()
    {
        if (Input.Button != "create")
        {
            return Redirect("https://localhost:5000");

        }

        if (ModelState.IsValid)
        {
            var existingUser = await _userManager.FindByNameAsync(Input.Username);
            if (existingUser != null)
            {
                //This should indicate the account already exists.
                //We shouldn't redirect but rather propogate up that
                //the username already exists
                ModelState.AddModelError(string.Empty, LoginOptions.InvalidCreateAccountErrorMessage);
                return Page();
            }

            ApplicationUser user = new ApplicationUser 
            {
                UserName = Input.Username,
                Email = Input.Email
            };

            //No user found, create a new one
            var newUser = await _userManager.CreateAsync(user);
            if (newUser.Succeeded)
            {
                await _userManager.AddPasswordAsync(user, Input.Password);
                return Redirect("Account/Login");
            }
        }
        return Page();
    }

}