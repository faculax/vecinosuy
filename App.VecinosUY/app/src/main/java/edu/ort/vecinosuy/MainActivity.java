package edu.ort.vecinosuy;

import android.content.Context;
import android.content.SharedPreferences;
import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.widget.TextView;

public class MainActivity extends AppCompatActivity {

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

    }
}
