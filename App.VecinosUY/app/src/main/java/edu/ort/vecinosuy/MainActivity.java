package edu.ort.vecinosuy;

import android.accounts.Account;
import android.accounts.AccountManager;
import android.content.Context;
import android.content.Intent;
import android.content.SharedPreferences;
import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.util.Patterns;
import android.view.View;
import android.view.View.OnClickListener;
import android.widget.Button;
import android.widget.EditText;
import android.widget.TextView;

import com.android.volley.AuthFailureError;
import com.android.volley.Request;
import com.android.volley.RequestQueue;
import com.android.volley.Response;
import com.android.volley.VolleyError;
import com.android.volley.toolbox.StringRequest;
import com.android.volley.toolbox.Volley;

import org.json.JSONArray;
import org.json.JSONException;
import org.json.JSONObject;

import java.util.ArrayList;
import java.util.HashMap;
import java.util.Iterator;
import java.util.Map;
import java.util.regex.Pattern;

import logic.Repository;

import static android.R.attr.password;

public class MainActivity extends AppCompatActivity implements OnClickListener{

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_main);
        Bundle login_main_boundle = getIntent().getExtras();
        TextView helloMesagge = (TextView) findViewById(R.id.helloUser);
        String userName = "";
        String token = getString(R.string.vecinosUySessionToken);
        if(login_main_boundle != null) {
            userName = login_main_boundle.getString("userName");
            token = login_main_boundle.getString("token");
            helloMesagge.setText("Hola " + userName );
        }
        Button v=(Button)this.findViewById(R.id.announcementsBtn);
        v.setOnClickListener(this);

    }
    @Override
    public void onClick(View v) {
        switch (v.getId()) {
            case R.id.announcementsBtn:
                String serverAddr = getResources().getString(R.string.serverAddr) + "announcements/";
                RequestQueue queue = Volley.newRequestQueue(this);
                StringRequest stringRequest = new StringRequest(Request.Method.GET, serverAddr,
                        new Response.Listener<String>() {

                            @Override
                            public void onResponse(String response) {
                                JSONArray jsonArray;
                                ArrayList<String> announcements = new ArrayList<String>();
                                try {
                                    jsonArray = new JSONArray(response);
                                    Repository.getInstance().announcementBody.clear();
                                    for (int i = 0; i< jsonArray.length(); i++) {
                                        JSONObject jsonObject = (JSONObject)jsonArray.get(i);
                                        String title = jsonObject.getString("Title");
                                        String id = jsonObject.getString("AnnouncementId");
                                        String body = jsonObject.getString("Body");
                                        String image = jsonObject.getString("Image");
                                        announcements.add(id + ": " + title);
                                        Repository.getInstance().announcementBody.put(Integer.parseInt(id),body);
                                        Repository.getInstance().announcementImage.put(Integer.parseInt(id),image);
                                    }
                                } catch (JSONException e) {
                                }
                                catch (Exception e) {
                                    String ee = e.toString();
                                }
                                Intent i = new Intent(getApplicationContext(), AnnouncementActivity.class);
                                Bundle announcementsBoundle = new Bundle();
                                announcementsBoundle.putStringArrayList("announcements", announcements);
                                i.putExtras(announcementsBoundle);
                                startActivity(i);
                            }
                        }, new Response.ErrorListener() {
                    @Override
                    public void onErrorResponse(VolleyError error) {
                        //mTxtDisplay.setText("Error de logueo, reintente");
                    }
                })
                {
                    @Override
                    public Map<String, String> getHeaders() throws AuthFailureError {
                        String email = getLogedUserEmail();
                        Map<String, String>  params = new HashMap<String, String>();
                        params.put("TODO_PAGOS_TOKEN", email);
                        params.put("Content-Type", "application/json");

                        return params;
                    }
                };
                queue.add(stringRequest);
              //  finish();
                break;
        }

    }
    private String getLogedUserEmail(){
        Account[] accounts = AccountManager.get(this).getAccounts();
        Pattern emailPattern = Patterns.EMAIL_ADDRESS;
        String possibleEmail = "";

        for (Account account : accounts) {
            if (emailPattern.matcher(account.name).matches()) {
                possibleEmail = account.name;
            }
        }
        return possibleEmail;
    }
}
