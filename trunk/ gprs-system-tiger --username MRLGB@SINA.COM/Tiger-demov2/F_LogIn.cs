﻿#region Using Statements
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using Tiger.Helper;
#endregion

namespace Tiger
{
  /// <summary>
  /// The login form
  /// </summary>
  public partial class F_Login: Form
  {

    #region Fields

    // the value of fails
    private int _countLogFailed;

    // the value of permissions on the error at one login procedure 
    private int _logins;

    // the flag of validate
    private bool _ValidForm;

    #endregion

    #region Initialization

    public F_Login()
    {
      InitializeComponent();
      _countLogFailed = 0;
      _logins = 3;
      tbName.Validating += new CancelEventHandler(ValidateTextBox);
      tbPassword.Validating += new CancelEventHandler(ValidateTextBox);
    }

    private void frmLogIn_Load( object sender, EventArgs e )
    {
        loginwork.Initialization();
    }

    #endregion

    #region Validating

    private void ValidateTextBox( object sender, CancelEventArgs e )
    {
      bool NameValid = true, PasswordValid = true;

      if (String.IsNullOrEmpty(((TextBox)sender).Text))
      {
        switch (Convert.ToByte(((TextBox)sender).Tag))
        {
          case 0:
            errorProvider1.SetError(tbName, "Please, enter your name");
            NameValid = false;
            break;
          case 1:
            errorProvider1.SetError(tbPassword, "Please, enter your password");
            PasswordValid = false;
            break;
        }
      }
      else
      {
        switch (Convert.ToByte(((TextBox)sender).Tag))
        {
          case 0:
            errorProvider1.SetError(tbName, "");
            break;
          case 1: errorProvider1.SetError(tbPassword, "");
            break;
        }
      }
      _ValidForm = NameValid && PasswordValid;
    }

    #endregion

    #region Events Click
    private void btnLogin_Click( object sender, EventArgs e )
    {

      if (_ValidForm)
      {
        //Check the nikname and the password
          loginwork.DoLogin(tbName.Text, tbPassword.Text);

          if (loginwork.Logged) //check the logged flag
        {
          this.Close();
        }
        else
        {
          _countLogFailed++;
          if (_countLogFailed > _logins - 1)
          {
            //You can do to close login form or do waiting user for instance 1 minute
            MessageBox.Show("You entered wrong password or nikname 3 times. \n You can do login after 1 minute");

            Thread.Sleep(60000);
            return;

            //this.Close();
          }
          MessageBox.Show("The password or the nik name are wrong. \n Please, try again. \n Remaining logins: "
            + (_logins - _countLogFailed).ToString(), "Login failed", MessageBoxButtons.OK);
        }
      }
      else
        MessageBox.Show("Please, fill all text boxes");

    }

    private void btnCancel_Click( object sender, EventArgs e )
    {
      this.Close();
    }
    #endregion


  }
}
