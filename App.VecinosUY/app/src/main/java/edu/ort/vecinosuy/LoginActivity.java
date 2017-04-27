package edu.ort.vecinosuy;

import android.accounts.Account;
import android.accounts.AccountManager;
import android.content.Context;
import android.content.DialogInterface;
import android.content.Intent;
import android.content.SharedPreferences;
import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.util.Patterns;
import android.widget.Button;
import android.view.View.OnClickListener;
import android.view.View;
import android.app.AlertDialog;
import android.widget.EditText;
import android.widget.TextView;

import com.android.volley.Request;
import com.android.volley.RequestQueue;
import com.android.volley.Response;
import com.android.volley.VolleyError;
import com.android.volley.toolbox.StringRequest;
import com.android.volley.toolbox.Volley;

import org.json.JSONException;
import org.json.JSONObject;

import java.util.regex.Pattern;

import static android.R.attr.password;
import static edu.ort.vecinosuy.R.string.serverAddr;


public class LoginActivity extends AppCompatActivity implements OnClickListener{

    Pattern emailPattern = Patterns.EMAIL_ADDRESS;
    String possibleEmail = "email";


    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_login);
        Button v=(Button)this.findViewById(R.id.entrarBtn);
        v.setOnClickListener(this);
        Account[] accounts = AccountManager.get(this).getAccounts();


        for (Account account : accounts) {
            if (emailPattern.matcher(account.name).matches()) {
                possibleEmail = account.name;
            }
        }
        SharedPreferences sharedPref = this.getPreferences(Context.MODE_PRIVATE);
        String defaultValue = getResources().getString(R.string.vecinosUySessionToken);
        String token = sharedPref.getString(getString(R.string.vecinosUySessionToken), defaultValue);
        if (!defaultValue.equals(token)) {
            validateToken(token);
        }
    }

    @Override
    public void onClick(View v) {
        switch (v.getId()) {
            case R.id.entrarBtn:
                    newUserFlow();
                break;
        }

    }
    private void newUserFlow(){
        String serverAddr = getString(R.string.serverAddr) + "users/";
        final TextView mTxtDisplay = (TextView) findViewById(R.id.userMessage);
        EditText passwordField   = (EditText)findViewById(R.id.passwordTxt);
        String password = passwordField.getText().toString();
        RequestQueue queue = Volley.newRequestQueue(this);
        String url = serverAddr + possibleEmail + "/login/" + password;
        StringRequest stringRequest = new StringRequest(Request.Method.GET, url,
                new Response.Listener<String>() {
                    @Override
                    public void onResponse(String response) {
                        callMainActivity(response);
                    }
                }, new Response.ErrorListener() {
            @Override
            public void onErrorResponse(VolleyError error) {
                mTxtDisplay.setText("Error de logueo, reintente");
            }
        });
        queue.add(stringRequest);
    }

    private void validateToken(String token) {
        String serverAddr = getString(R.string.serverAddr) + "users/";
        RequestQueue queue = Volley.newRequestQueue(this);
        final TextView mTxtDisplay = (TextView) findViewById(R.id.userMessage);
        String url = serverAddr + possibleEmail + "/validateToken/" + token;
        StringRequest stringRequest = new StringRequest(Request.Method.GET, url,
                new Response.Listener<String>() {
                    @Override
                    public void onResponse(String response) {
                        callMainActivity(response);
                    }
                }, new Response.ErrorListener() {
            @Override
            public void onErrorResponse(VolleyError error) {
                mTxtDisplay.setText("Error de token, contacte al admin");
            }
        });
        queue.add(stringRequest);
    }

    private void callMainActivity(String response) {
        JSONObject jsonObj;
        String userName = "usuario";
        try {
            jsonObj = new JSONObject(response);
            userName = jsonObj.getString("Name");
        } catch (JSONException e) {}
        Intent i = new Intent(getApplicationContext(), MainActivity.class);
        Bundle loginMainBoundle = new Bundle();
        loginMainBoundle.putString("userName", userName);
        i.putExtras(loginMainBoundle);
        startActivity(i);
        finish();
    }
}
