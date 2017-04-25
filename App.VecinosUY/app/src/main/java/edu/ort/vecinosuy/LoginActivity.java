package edu.ort.vecinosuy;

import android.accounts.Account;
import android.accounts.AccountManager;
import android.content.DialogInterface;
import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.util.Patterns;
import android.widget.Button;
import android.view.View.OnClickListener;
import android.view.View;
import android.app.AlertDialog;
import android.widget.EditText;

import java.util.regex.Pattern;


public class LoginActivity extends AppCompatActivity implements OnClickListener{

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_login);
        Button v=(Button)this.findViewById(R.id.entrarBtn);
        v.setOnClickListener(this);

    }

    @Override
    public void onClick(View v) {
        switch (v.getId()) {
            case R.id.entrarBtn:
                Pattern emailPattern = Patterns.EMAIL_ADDRESS; // API level 8+
                Account[] accounts = AccountManager.get(this).getAccounts();
                EditText passwordField   = (EditText)findViewById(R.id.passwordTxt);
                String password = passwordField.getText().toString();
                String possibleEmail = "email";
                for (Account account : accounts) {
                    if (emailPattern.matcher(account.name).matches()) {
                        possibleEmail = account.name;
                    }
                }
                String serverAddr = getString(R.string.serverAddr);
                new AlertDialog.Builder(this)
                        .setTitle(possibleEmail + " " + serverAddr)
                        .setMessage("Are you sure you want to delete this entry?")
                        .setPositiveButton(android.R.string.yes, new DialogInterface.OnClickListener() {
                            public void onClick(DialogInterface dialog, int which) {
                                // continue with delete
                            }
                        })
                        .setNegativeButton(android.R.string.no, new DialogInterface.OnClickListener() {
                            public void onClick(DialogInterface dialog, int which) {
                                // do nothing
                            }
                        })
                        .setIcon(android.R.drawable.ic_dialog_alert)
                        .show();
                break;
        }

    }}
