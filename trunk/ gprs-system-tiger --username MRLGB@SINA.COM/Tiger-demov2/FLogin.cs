using System;
using System.ComponentModel;
using System.Windows.Forms;
using System.Threading;
using Tiger.Helper;

namespace Tiger
{
  /// <summary>
  /// The login form
  /// </summary>
  public partial class FLogin: Form
  {

    #region Fields

    // the value of fails
    private int _countLogFailed;

    // the value of permissions on the error at one login procedure 
    private readonly int _logins;

    // the flag of validate
    private bool _validForm;

    #endregion

    #region Initialization

    public FLogin()
    {
      InitializeComponent();
      _countLogFailed = 0;
      _logins = 3;
      tbName.Validating += new CancelEventHandler(ValidateTextBox);
      tbPassword.Validating += new CancelEventHandler(ValidateTextBox);
    }

    private void FrmLogInLoad( object sender, EventArgs e )
    {
        Loginwork.Initialization();
    }

    #endregion

    #region Validating

    private void ValidateTextBox( object sender, CancelEventArgs e )
    {
      bool nameValid = true, passwordValid = true;

      if (String.IsNullOrEmpty(((TextBox)sender).Text))
      {
        switch (Convert.ToByte(((TextBox)sender).Tag))
        {
          case 0:
            errorProvider1.SetError(tbName, "Please, enter your name");
            nameValid = false;
            break;
          case 1:
            errorProvider1.SetError(tbPassword, "Please, enter your password");
            passwordValid = false;
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
      _validForm = nameValid && passwordValid;
    }

    #endregion

    #region Events Click
    private void BtnLoginClick( object sender, EventArgs e )
    {

      if (_validForm)
      {
        //Check the nikname and the password
          Loginwork.DoLogin(tbName.Text, tbPassword.Text);

          if (Loginwork.Logged) //check the logged flag
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

    private void BtnCancelClick( object sender, EventArgs e )
    {
      this.Close();
    }
    #endregion


  }
}
