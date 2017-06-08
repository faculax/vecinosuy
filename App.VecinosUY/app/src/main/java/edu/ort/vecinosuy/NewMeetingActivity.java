package edu.ort.vecinosuy;

import android.accounts.Account;
import android.accounts.AccountManager;
import android.app.DialogFragment;
import android.content.Intent;
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

import java.sql.Timestamp;
import java.util.Calendar;
import java.util.Date;
import java.util.HashMap;
import java.util.Map;
import java.util.regex.Pattern;


public class NewMeetingActivity extends AppCompatActivity implements View.OnClickListener {

    String subject;

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
        subject = subjectField.getText().toString();
        params.put("Subject", subject);
        params.put("Deleted", "false");
        String date = Repository.getInstance().year + "-" + (Repository.getInstance().month+1) + "-" + Repository.getInstance().day +
                "T" + Repository.getInstance().hour + ":" + Repository.getInstance().minute + ":00";
        //params.put("Date", "2016-10-23T11:02:44");
        params.put("Date", date);
        JsonObjectRequest request_json = new JsonObjectRequest(serverAddr, new JSONObject(params),
                new Response.Listener<JSONObject>() {
                    @Override
                    public void onResponse(JSONObject response) {
                        setEventOnCalendar();
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

    public void setEventOnCalendar(){
        int month = Repository.getInstance().month;
        String sMonth = "";
        if (month<10) {
            sMonth = "0"+month;
        } else {
            sMonth = ""+month;
        }
        int day = Repository.getInstance().day;
        String sday = "";
        if (day<10) {
            sday = "0"+day;
        } else {
            sday = ""+day;
        }
        int hour = Repository.getInstance().hour;
        String shour = "";
        if (hour<10) {
            shour = "0"+hour;
        } else {
            shour = ""+hour;
        }
        int minute = Repository.getInstance().minute;
        String sMinute = "";
        if (minute<10) {
            sMinute = "0"+minute;
        } else {
            sMinute = ""+minute;
        }
            java.sql.Timestamp tsStart = java.sql.Timestamp.valueOf(Repository.getInstance().year + "-" +
                    sMonth+ "-" + sday + " " + shour + ":" + sMinute + ":00");
            long startTime = tsStart.getTime();
            Calendar cal = Calendar.getInstance();
            cal.setTime(tsStart);
            cal.add(Calendar.HOUR_OF_DAY, 1);
            tsStart.setTime(cal.getTime().getTime()); // or
            tsStart = new Timestamp(cal.getTime().getTime());

        long endTime = tsStart.getTime();

        Intent intent = new Intent(Intent.ACTION_EDIT);
        intent.putExtra("calendar_id", 1);
        intent.setType("vnd.android.cursor.item/event");
        intent.putExtra("beginTime", startTime);
        intent.setFlags(Intent.FLAG_ACTIVITY_REORDER_TO_FRONT |
                Intent.FLAG_ACTIVITY_SINGLE_TOP);
        intent.putExtra("allDay", false);
        intent.putExtra("endTime", endTime);
        intent.putExtra("title", subject);
        intent.putExtra("description", subject);
        startActivity(intent);



       /* Calendar cal = Calendar.getInstance();
        Date date = new Date(Repository.getInstance().year,Repository.getInstance().month,Repository.getInstance().day,
                Repository.getInstance().hour,Repository.getInstance().minute);
        cal.setTime(date); // sets calendar time/date
        cal.add(Calendar.HOUR_OF_DAY, 1); // adds one hour
        Date d = cal.getTime();
        Intent intent = new Intent(Intent.ACTION_EDIT);
        intent.setType("vnd.android.cursor.item/event");
        intent.putExtra("beginTime", date.getTime());
        intent.putExtra("allDay", false);
        intent.putExtra("endTime", d.getTime());
        intent.putExtra("title", subject);
        startActivity(intent); */
    }
}
