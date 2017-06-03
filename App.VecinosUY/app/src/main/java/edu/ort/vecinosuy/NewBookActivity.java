package edu.ort.vecinosuy;

import android.accounts.Account;
import android.accounts.AccountManager;
import android.app.AlertDialog;
import android.app.DialogFragment;
import android.os.Bundle;
import android.support.v7.app.AppCompatActivity;
import android.text.method.DateTimeKeyListener;
import android.util.Patterns;
import android.view.View;
import android.widget.ArrayAdapter;
import android.widget.Button;
import android.widget.EditText;
import android.widget.Spinner;

import com.android.volley.AuthFailureError;
import com.android.volley.RequestQueue;
import com.android.volley.Response;
import com.android.volley.VolleyError;
import com.android.volley.VolleyLog;
import com.android.volley.toolbox.JsonObjectRequest;
import com.android.volley.toolbox.Volley;

import org.json.JSONObject;

import java.util.ArrayList;
import java.util.Calendar;
import java.util.Date;
import java.util.HashMap;
import java.util.List;
import java.util.Map;
import java.util.regex.Pattern;

import logic.DatePickerFragment;
import logic.Repository;
import logic.TimePickerFragment;

import static android.R.id.list;
import static edu.ort.vecinosuy.R.string.serverAddr;


public class NewBookActivity extends AppCompatActivity implements View.OnClickListener {
    private Spinner spinner1, servicesSpinner;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_new_book);
        getSupportActionBar().setDisplayHomeAsUpEnabled(true);
        getSupportActionBar().setDisplayShowHomeEnabled(true);
        Button v=(Button)this.findViewById(R.id.bookDateBtn);
        v.setOnClickListener(this);
        v=(Button)this.findViewById(R.id.bookTimeBtn);
        v.setOnClickListener(this);
        v=(Button)this.findViewById(R.id.scheduleBookBtn);
        v.setOnClickListener(this);
        addItemsOnSpinner();
        addItemsOnServicesSpinner();
    }
    public void addItemsOnSpinner() {

        spinner1 = (Spinner) findViewById(R.id.spinner1);
        List<String> list = new ArrayList<String>();
        list.add("1");
        list.add("2");
        list.add("4");
        list.add("8");
        ArrayAdapter<String> dataAdapter = new ArrayAdapter<String>(this,
                android.R.layout.simple_spinner_item, list);
        dataAdapter.setDropDownViewResource(android.R.layout.simple_spinner_dropdown_item);
        spinner1.setAdapter(dataAdapter);
    }

    public void addItemsOnServicesSpinner() {

        servicesSpinner = (Spinner) findViewById(R.id.servicesSpinner);
        ArrayAdapter<String> dataAdapter = new ArrayAdapter<String>(this,
                android.R.layout.simple_spinner_item, Repository.getInstance().servicesRepository);
        dataAdapter.setDropDownViewResource(android.R.layout.simple_spinner_dropdown_item);
        servicesSpinner.setAdapter(dataAdapter);
    }



    @Override
    public void onClick(View v) {
        switch (v.getId()) {
            case R.id.scheduleBookBtn:
                postBooking(v);
    //            //  finish();
                break;
            case R.id.bookDateBtn:
                showDatePickerDialog(v);
                //  finish();
                break;
            case R.id.bookTimeBtn:
                showTimePickerDialog(v);
                //  finish();
                break;
        }

    }

    public void showTimePickerDialog(View v) {
        DialogFragment newFragment = new TimePickerFragment();
        newFragment.show(getFragmentManager(), "timePicker");
    }

    private void postBooking(View v) {

        if (Repository.getInstance().validTimeAndDate()) {
            postBooking();
         //   finish();

        } else {

            new AlertDialog.Builder(this)
                    .setTitle("Validacion de fechas")
                    .setMessage("Ingrese fecha y hora antes de continuar")
                    .setCancelable(true)
                    .setIcon(android.R.drawable.ic_dialog_alert)
                    .show();
        }
    }

    private void postBooking(){
        servicesSpinner = (Spinner) findViewById(R.id.servicesSpinner);
        spinner1 = (Spinner) findViewById(R.id.spinner1);
        String hours = (String)spinner1.getSelectedItem();
        String service = (String)servicesSpinner.getSelectedItem();
        Date d = new Date(Repository.getInstance().year,
                Repository.getInstance().month,
                Repository.getInstance().day,
                Repository.getInstance().hour,
                Repository.getInstance().minute);
        Calendar cal = Calendar.getInstance(); // creates calendar
        cal.setTime(d); // sets calendar time/date
        cal.add(Calendar.HOUR_OF_DAY, Integer.parseInt(hours)); // adds one hour
        d = cal.getTime();
        String serverAddr = getResources().getString(R.string.serverAddr) + "bookings/";
        RequestQueue queue = Volley.newRequestQueue(this);
        HashMap<String, String> params = new HashMap<String, String>();
        params.put("User", getLogedUserEmail());
        params.put("Service", service);
        String date = Repository.getInstance().year + "-" + (Repository.getInstance().month+1) + "-" + Repository.getInstance().day +
                "T" + Repository.getInstance().hour + ":" + Repository.getInstance().minute + ":00";
        params.put("BookedFrom", date);
        int monthTo = d.getMonth()+1;
        String dateTo = d.getYear() + "-" + monthTo + "-" + d.getDate() +
                "T" + d.getHours() + ":" + d.getMinutes() + ":00";
        params.put("BookedTo", dateTo);
        params.put("Deleted", "false");
        JsonObjectRequest request_json = new JsonObjectRequest(serverAddr, new JSONObject(params),
                new Response.Listener<JSONObject>() {

                    @Override
                    public void onResponse(JSONObject response) {
                        finish();
                    }
                }, new Response.ErrorListener() {
            @Override
            public void onErrorResponse(VolleyError error) {
                String msg = "error";
                if(error.networkResponse != null && error.networkResponse.data != null){
                    msg = new String(error.networkResponse.data);
                    showError(msg);
                }
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


    public void showDatePickerDialog(View v) {
        DialogFragment newFragment = new DatePickerFragment();
        newFragment.show(getFragmentManager(), "datePicker");
    }

    @Override
    public boolean onSupportNavigateUp() {
        onBackPressed();
        return true;
    }

    public void showError(String msg){
        new AlertDialog.Builder(this)
                .setTitle("Error de reserva")
                .setMessage(msg)
                .setCancelable(true)
                .setIcon(android.R.drawable.ic_dialog_alert)
                .show();
    }
}
