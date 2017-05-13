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
import com.google.android.gms.common.ConnectionResult;
import com.google.firebase.messaging.FirebaseMessaging;

import org.json.JSONException;
import org.json.JSONObject;

import java.util.regex.Pattern;

import static android.R.attr.password;
import static android.icu.text.RelativeDateTimeFormatter.Direction;
import static edu.ort.vecinosuy.R.string.serverAddr;


public class LoginActivity extends AppCompatActivity implements OnClickListener{

    Pattern emailPattern = Patterns.EMAIL_ADDRESS;
    String possibleEmail = "email";
    public static String SERVER_DOWN = "sin conexion!";


    @Override
    protected void onCreate(Bundle savedInstanceState) {
       // GoogleApiAvailability.makeGooglePlayServicesAvailable();
        FirebaseMessaging.getInstance().subscribeToTopic("allDevices");
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
        String serverAddr = getResources().getString(R.string.serverAddr) + "users/";
        final TextView mTxtDisplay = (TextView) findViewById(R.id.userMessage);
        EditText passwordField   = (EditText)findViewById(R.id.passwordTxt);
        String password = passwordField.getText().toString();
        RequestQueue queue = Volley.newRequestQueue(this);
        String url = serverAddr + possibleEmail + "/loginUser/" + password;
        StringRequest stringRequest = new StringRequest(Request.Method.GET, url,
                new Response.Listener<String>() {
                    @Override
                    public void onResponse(String response) {
                        JSONObject jsonObj;
                        String sessionToken = getString(R.string.vecinosUySessionToken);
                        try {
                            jsonObj = new JSONObject(response);
                            sessionToken = jsonObj.getString("Token");
                        } catch (JSONException e) {}
                        callMainActivity(response, sessionToken, false);
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
        String serverAddr = getResources().getString(R.string.serverAddr) + "users/";
        RequestQueue queue = Volley.newRequestQueue(this);
        final TextView mTxtDisplay = (TextView) findViewById(R.id.userMessage);
        String url = serverAddr + possibleEmail + "/validateToken/" + token;
        StringRequest stringRequest = new StringRequest(Request.Method.GET, url,
                new Response.Listener<String>() {
                    @Override
                    public void onResponse(String response) {
                        JSONObject jsonObj;
                        String token = getResources().getString(R.string.vecinosUySessionToken);
                        try {
                            jsonObj = new JSONObject(response);
                            token = jsonObj.getString("Token");
                        } catch (JSONException e) {}
                        callMainActivity(response, token, false);
                    }
                }, new Response.ErrorListener() {
            @Override
            public void onErrorResponse(VolleyError error) {
                if (error.networkResponse == null) {
                    callMainActivity("", "", true);
                } else {
                    mTxtDisplay.setText("Ha sido deslogueado por el admin, reingrese");
                }
            }
        });
        queue.add(stringRequest);
    }

    private void callMainActivity(String response, String token, boolean serverDown) {
        SharedPreferences sharedPref = this.getPreferences(Context.MODE_PRIVATE);
        SharedPreferences.Editor editor = sharedPref.edit();
        editor.putString(getString(R.string.vecinosUySessionToken), token);
        editor.commit();
        JSONObject jsonObj;
        String userName = "usuario";
        try {
            jsonObj = new JSONObject(response);
            userName = jsonObj.getString("Name");
        } catch (JSONException e) {}
        Intent i = new Intent(getApplicationContext(), MainActivity.class);
        Bundle loginMainBoundle = new Bundle();
        if (serverDown) {
            userName = this.SERVER_DOWN;
        }
        loginMainBoundle.putString("userName", userName);
        loginMainBoundle.putString("token", token);
        i.putExtras(loginMainBoundle);
        startActivity(i);
        finish();
    }
}
