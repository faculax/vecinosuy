package logic;

import java.util.ArrayList;
import java.util.HashMap;
import java.util.List;
import java.util.Map;

/**
 * Created by Facu on 5/9/17.
 */

public class Repository {
    public Map<Integer,String> announcementBody;
    public Map<Integer,String> announcementImage;
    public Map<Integer,voteDto> votesRepository;
    public List <String> servicesRepository;
    private int minute = -1;

    public void setMinute(int minute) {
        this.minute = minute;
    }

    public void setHour(int hour) {
        this.hour = hour;
    }

    public void setMonth(int month) {
        this.month = month;
    }

    public void setYear(int year) {
        this.year = year;
    }

    public void setDay(int day) {
        this.day = day;
    }

    private int hour = -1;
    private int month = -1;
    private int year = -1;
    private int day = -1;
    private static Repository instance = null;
    protected Repository() {
        announcementBody = new HashMap<Integer, String>();
        announcementImage = new HashMap<Integer, String>();
        votesRepository = new HashMap<Integer, voteDto>();
        servicesRepository = new ArrayList<String>();
    }
    public static Repository getInstance() {
        if(instance == null) {
            instance = new Repository();
        }
        return instance;
    }

    public void reset(){
         minute = -1;
         hour = -1;
         month = -1;
         year = -1;
         day = -1;
    }

    public boolean validTimeAndDate(){
        return minute != -1 &&
                hour != -1 &&
                month != -1 &&
                year != -1 &&
                day != -1;
    }

    public boolean validDate(){
        return month != -1 &&
                year != -1 &&
                day != -1;
    }

    public String getMinute() {
        if (minute<10){
            return "0"+minute;
        }
        return minute+"";
    }

    public String getHour() {
        if (hour<10){
            return "0"+hour;
        }
        return hour+"";
    }

    public String getMonth() {
        if (month<10){
            return "0"+month;
        }
        return month+"";
    }

    public String getYear() {
        return year+"";
    }

    public String getDay() {
        if (day<10){
            return "0"+day;
        }
        return day+"";
    }
}
