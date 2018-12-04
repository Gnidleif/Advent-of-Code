package day04;

import java.io.IOException;
import java.nio.file.Files;
import java.nio.file.Paths;
import java.text.ParseException;
import java.text.SimpleDateFormat;
import java.util.ArrayList;
import java.util.Collections;
import java.util.Date;
import java.util.HashMap;
import java.util.Map;
import java.util.regex.Matcher;
import java.util.regex.Pattern;

public class Day04 {
    public static void main(String[] args) {
        String data = "";
        try {
            data = new String(Files.readAllBytes(Paths.get("Day04.txt")));
        } catch (IOException e) {
            System.out.println(e);
        }
        
        Pattern rgx = Pattern.compile("^\\[((\\d+).*)\\]\\s(.*)$", Pattern.MULTILINE);
        Matcher m = rgx.matcher(data);
        SimpleDateFormat fmt = new SimpleDateFormat("yyyy-MM-dd HH:mm");
        ArrayList<DateLine> guardLog = new ArrayList<>();

        try {
            while(m.find()) {
                guardLog.add(new DateLine(fmt.parse(m.group(1)), m.group(3)));
            }
            Collections.sort(guardLog);
            HashMap<Integer, Guard> guards = CreateGuards(guardLog);
            
            System.out.println(Part1(guards));
            System.out.println(Part2(guards));
        } catch(ParseException e) {
            System.out.println(e);
        }
    }

    public static int Part1(HashMap<Integer, Guard> guards) {
        int bestGuard = 0;
        int bestTotal = 0;
        for(Map.Entry<Integer, Guard> pair : guards.entrySet()) {
            if (pair.getValue().totalSleep > bestTotal) {
                bestTotal = pair.getValue().totalSleep;
                bestGuard = pair.getKey();
            }
        }
        return bestGuard * guards.get(bestGuard).GetBestMinute();
    }
    
    public static int Part2(HashMap<Integer, Guard> guards) {
        int bestGuard = -1;
        int bestFrequency = 0;
        for(Map.Entry<Integer, Guard> pair : guards.entrySet()) {
            pair.getValue().GetBestMinute();
            if(bestGuard < 0) {
                bestGuard = pair.getKey();
                bestFrequency = pair.getValue().bestFrequency;
            }
            if(pair.getValue().bestFrequency > bestFrequency) {
                bestGuard = pair.getKey();
                bestFrequency = pair.getValue().bestFrequency;
            }
        }
        return bestGuard * guards.get(bestGuard).bestMinute;
    }
        
    public static HashMap<Integer, Guard> CreateGuards(ArrayList<DateLine> guardLog) {
        HashMap<Integer, Guard> guards = new HashMap<>();
        int start = 0;
        int currentId = 0;
        for(DateLine entry : guardLog) {
            String[] words = entry.command.split(" ");
            switch(words[0]) {
                case "Guard":
                    int id = Integer.parseInt(words[1].substring(1));
                    if(!guards.containsKey(id)) {
                        guards.put(id, new Guard());
                    }
                    if(currentId != id) {
                        currentId = id;
                        guards.get(currentId).shiftCount++;
                    }
                    break;
                case "falls":
                    start = entry.dateTime.getMinutes();
                    break;
                case "wakes":
                    guards.get(currentId).ToggleSleep(start, entry.dateTime.getMinutes());
                    break;
            }
        }
        return guards;
    }

    public static class DateLine implements Comparable<DateLine> {
        public Date dateTime;
        public String command;

        public DateLine(Date dt, String cmd) {
            this.dateTime = dt;
            this.command = cmd;
        }

        @Override
        public int compareTo(DateLine o) {
            return this.dateTime.compareTo(o.dateTime);
        }

        @Override
        public String toString() {
            return this.dateTime.toString() + ": " + this.command;
        }
    }

    public static class Guard {
        public int shiftCount = 1;
        public int totalSleep = 0;
        public int[] minutes = new int[60];
        public int bestFrequency = 0;
        public int bestMinute = 0;

        public void ToggleSleep(int start, int end) {
            totalSleep += (end-start);
            for(int i = start; i < end; i++) {
                this.minutes[i]++;
            }
        }
        
        public int GetBestMinute() {
            for(int i = 0; i < this.minutes.length; i++) {
                if(this.minutes[i] > this.bestFrequency) {
                    this.bestFrequency = this.minutes[i];
                    this.bestMinute = i;
                }
            }
            return this.bestMinute;
        }
    }
}