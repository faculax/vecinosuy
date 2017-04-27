package edu.ort.vecinosuy;

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
        if(login_main_boundle != null) {
            userName = login_main_boundle.getString("userName");
            helloMesagge.setText("Hola " + userName );
        }

    }
}
