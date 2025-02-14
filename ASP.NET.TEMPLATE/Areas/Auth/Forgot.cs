﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace ASP.NET.TEMPLATE.Areas.Auth
{
    public class Forgot : BaseModel
    {
        #region Data

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email address")]
        public string Email { get; set; }

        #endregion

        #region Handlers

        public override async Task<IActionResult> PostAsync()
        {
            if (!ModelState.IsValid) return View(this);

            var user = await _db.User.SingleAsync(u => u.Email == Email && u.IsDeleted == false);
            if (user != null)
            {
                // Generate reset token. 
                user.ResetCode = Crypto.RandomString(7);
                user.ResetDate = null;

                _db.User.Update(user);
                await _db.SaveChangesAsync();

                _email.SendResetMessage(user);

                return RedirectToAction("ForgotConfirm");
            }

            Failure = "Incorrect email. Please try again";

            return View(this);
        }

        #endregion
    }
}