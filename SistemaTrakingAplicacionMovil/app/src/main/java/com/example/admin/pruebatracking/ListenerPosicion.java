package com.example.admin.pruebatracking;

import android.content.Context;
import android.location.Location;
import android.location.LocationListener;
import android.os.Bundle;
import android.widget.Toast;

/**
 * Created by Casa on 29/10/2015.
 */
public class ListenerPosicion implements LocationListener {

    Context context;

    public ListenerPosicion(Context context) {

        this.context = context;

    }

    @Override
    public void onLocationChanged(Location location) {

        Toast.makeText(context, "Latitud: " + location.getLatitude() + " Longitud: " + location.getLongitude(), Toast.LENGTH_LONG).show();
    }

    @Override
    public void onProviderDisabled(String provider) {

    }

    @Override
    public void onProviderEnabled(String provider) {

    }

    @Override
    public void onStatusChanged(String provider, int status, Bundle extras) {

    }
}
