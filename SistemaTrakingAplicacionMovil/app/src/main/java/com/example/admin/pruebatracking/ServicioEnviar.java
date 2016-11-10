package com.example.admin.pruebatracking;

import java.io.BufferedReader;
import java.io.InputStreamReader;
import java.io.PrintWriter;
import java.net.Socket;
import java.util.ArrayList;

import android.app.Activity;
import android.content.Context;
import android.content.pm.PackageManager;
import android.graphics.drawable.AnimationDrawable;
import android.location.Location;
import android.location.LocationListener;
import android.location.LocationManager;
import android.location.LocationProvider;
import android.os.AsyncTask;
import android.os.Bundle;
import android.support.v4.content.ContextCompat;
import android.util.Log;
import android.widget.Button;
import android.widget.TextView;
import android.widget.Toast;

import com.example.admin.pruebatracking.Mensajes.Mensaje;
import com.example.admin.pruebatracking.Mensajes.MsgConexion;
import com.example.admin.pruebatracking.Mensajes.MsgDBPeticion;
import com.example.admin.pruebatracking.Mensajes.MsgDBRespuesta;
import com.example.admin.pruebatracking.Mensajes.MsgLocalizacion;
import com.example.admin.pruebatracking.Mensajes.MsgNotificacion;
import com.example.admin.pruebatracking.Serializacion.Serializacion;



public class ServicioEnviar implements LocationListener {

    Context context;

    public ServicioEnviar(Context context) {

        this.context = context;
    }

    public void enviarMensaje(Mensaje msg)
    {
        try {
            if(((AplicacionPrincipal) context).getSocket() != null) {
                if (((AplicacionPrincipal) context).getSocket().isConnected() && ((AplicacionPrincipal) context).getConectado()) {

                    PrintWriter writer = new PrintWriter(((AplicacionPrincipal) context).getSocket().getOutputStream());

                    switch (msg.getTipo()){
                        case "MsgConexion":
                             writer.println(Serializacion.Serializar((MsgConexion)msg));
                            break;
                        case "MsgLocalizacion":
                            writer.println(Serializacion.Serializar((MsgLocalizacion) msg));
                            break;
                        case "MsgDBPeticion":
                            writer.println(Serializacion.Serializar((MsgDBPeticion) msg));
                            break;
                        case "MsgDBRespuesta":
                            writer.println(Serializacion.Serializar((MsgDBRespuesta) msg));
                            break;
                        case "MsgNotificacion":
                            writer.println(Serializacion.Serializar((MsgNotificacion) msg));
                            break;
                    }
                    writer.flush();

                }
            }
        } catch (Exception e) {
            e.printStackTrace();
            /*((Activity) context).runOnUiThread(new Runnable() {
                @Override
                public void run() {
                    Toast.makeText(context, "ERROR EN SERVICIO ENVIAR", Toast.LENGTH_LONG).show();
                }
            });*/
        }
    }

    @Override
    public void onLocationChanged(Location location) {

        try {
            if(((AplicacionPrincipal) context).getSocket() != null) {
                if (((AplicacionPrincipal) context).getSocket().isConnected() && ((AplicacionPrincipal) context).getConectado()) {

                    PrintWriter writer = new PrintWriter(((AplicacionPrincipal) context).getSocket().getOutputStream());
                    ArrayList<String> arrayDestino = new ArrayList<String>();
                    arrayDestino.add("Escritorio");
                    writer.println(Serializacion.Serializar(new MsgLocalizacion(arrayDestino, "yo", "2016-10-27", location.getLatitude() + "", location.getLongitude() + "")));
                    writer.flush();

                }
            }
        } catch (Exception e) {
            e.printStackTrace();
            /*((Activity) context).runOnUiThread(new Runnable() {
                @Override
                public void run() {
                    Toast.makeText(context, "ERROR EN SERVICIO ENVIAR LOCALIZACION", Toast.LENGTH_LONG).show();
                }
            });*/
        }

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
