package edu.ort.vecinosuy;

import android.app.DialogFragment;
import android.content.DialogInterface;
import android.support.v4.app.FragmentActivity;
import android.support.v7.app.ActionBarActivity;
import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.view.View;
import android.widget.Button;

import logic.DatePickerFragment;
import logic.Repository;
import logic.TimePickerFragment;
import android.app.AlertDialog.*;
import android.app.AlertDialog;



public class MeetingActivity extends AppCompatActivity implements View.OnClickListener {

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_meeting);
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
