package edu.ort.vecinosuy;

import android.accounts.Account;
import android.accounts.AccountManager;
import android.app.DialogFragment;
import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.util.Patterns;
import android.view.View;
import android.widget.Button;

import logic.DatePickerFragment;
import logic.Repository;
import logic.TimePickerFragment;

import android.app.AlertDialog;
import android.widget.EditText;

import com.android.volley.AuthFailureError;
import com.android.volley.RequestQueue;
import com.android.volley.Response;
import com.android.volley.VolleyError;
import com.android.volley.VolleyLog;
import com.android.volley.toolbox.JsonObjectRequest;
import com.android.volley.toolbox.Volley;

import org.json.JSONObject;

import java.util.HashMap;
import java.util.Map;
import java.util.regex.Pattern;


public class NewMeetingActivity extends AppCompatActivity implements View.OnClickListener {

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_new_meeting);
        getSupportActionBar().setDisplayHomeAsUpEnabled(true);
        getSupportActionBar().setDisplayShowHomeEnabled(true);
        Button v=(Button)this.findViewById(R.id.meetingTimeBtn);
        v.setOnClickListener(this);
        v=(Button)this.findViewById(R.id.meetingDateBtn);
        v.setOnClickListener(this);
        v=(Button)this.findViewById(R.id.scheduleMeetingBtn);
        v.setOnClickListener(this);
    }

    @Override
    public void onClick(View v) {
        switch (v.getId()) {
            case R.id.meetingTimeBtn:
                showTimePickerDialog(v);
                //  finish();
                break;
            case R.id.meetingDateBtn:
                showDatePickerDialog(v);
                //  finish();
                break;
            case R.id.scheduleMeetingBtn:
                schedule(v);
                //  finish();
                break;
        }

    }

    private void schedule(View v) {
        if (Repository.getInstance().validTimeAndDate()) {
            // interactuar con la web api
            postMeeting();
            finish();

        } else {

            new AlertDialog.Builder(this)
                    .setTitle("Validacion de fechas")
                    .setMessage("Ingrese fecha y hora antes de continuar")
                    .setCancelable(true)
                    .setIcon(android.R.drawable.ic_dialog_alert)
                    .show();
        }
    }

    private void postMeeting(){
        String serverAddr = getResources().getString(R.string.serverAddr) + "meetings/";
        RequestQueue queue = Volley.newRequestQueue(this);
        HashMap<String, String> params = new HashMap<String, String>();
        EditText subjectField   = (EditText)findViewById(R.id.meetingTitle);
        String subject = subjectField.getText().toString();
        params.put("Subject", subject);
        params.put("Deleted", "false");
        String date = Repository.getInstance().year + "-" + Repository.getInstance().month + "-" + Repository.getInstance().day +
                "T" + Repository.getInstance().hour + ":" + Repository.getInstance().minute + ":00";
        //params.put("Date", "2016-10-23T11:02:44");
        params.put("Date", date);
        JsonObjectRequest request_json = new JsonObjectRequest(serverAddr, new JSONObject(params),
                new Response.Listener<JSONObject>() {
                    @Override
                    public void onResponse(JSONObject response) {
                        finish();
                    }
                }, new Response.ErrorListener() {
            @Override
            public void onErrorResponse(VolleyError error) {
                VolleyLog.e("Error: ", error.getMessage());
            }
        }

        ){
            @Override
            public Map<String, String> getHeaders() throws AuthFailureError {
                String email = getLogedUserEmail();
                Map<String, String>  params = new HashMap<String, String>();
                params.put("TODO_PAGOS_TOKEN", email);

                return params;
            }
        };
        queue.add(request_json);
        //  finish();
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

    public void showTimePickerDialog(View v) {
        DialogFragment newFragment = new TimePickerFragment();
        newFragment.show(getFragmentManager(), "timePicker");
    }

    public void showDatePickerDialog(View v) {
        DialogFragment newFragment = new DatePickerFragment();
        newFragment.show(getFragmentManager(), "datePicker");
    }

    @Override
    public boolean onSupportNavigateUp() {
        onBackPressed();
        return true;
    }
}
