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

import com.example.admin.pruebatracking.Entidades.Grupo;
import com.example.admin.pruebatracking.Mensajes.Mensaje;
import com.example.admin.pruebatracking.Mensajes.MsgConexion;
import com.example.admin.pruebatracking.Mensajes.MsgDBPeticion;
import com.example.admin.pruebatracking.Mensajes.MsgDBRespuesta;
import com.example.admin.pruebatracking.Mensajes.MsgLocalizacion;
import com.example.admin.pruebatracking.Mensajes.MsgNotificacion;
import com.example.admin.pruebatracking.Serializacion.Serializacion;



public class ServicioEnviar extends AsyncTask<Mensaje, Void, Void> implements LocationListener {

    Context context;
    AplicacionPrincipal global;

    public ServicioEnviar(Context context) {

        this.context = context;
        global = (AplicacionPrincipal) context.getApplicationContext();
    }

    protected Void doInBackground(Mensaje... arg)
    {
        Log.e("msg","entro al servicio enviar");
        try {
            if(global.getSocket() != null) {
                if (global.getSocket().isConnected() && (global.getConectado())) {

                    PrintWriter writer = new PrintWriter(global.getSocket().getOutputStream());
                    Log.e("msg","el tipo es: "+arg[0].getTipo());
                    switch (arg[0].getTipo()){
                        case "MsgConexion":
                             writer.println(Serializacion.Serializar((MsgConexion)arg[0]));
                            break;
                        case "MsgLocalizacion":
                            writer.println(Serializacion.Serializar((MsgLocalizacion) arg[0]));
                            break;
                        case "MsgDBPeticion":
                            switch (((MsgDBPeticion)arg[0]).getCodigoPeticion()) {
                                case "Login":
                                    Log.e("msg","Mensaje de peticion (Login)");
                                    ((AplicacionPrincipal) context.getApplicationContext()).setLogin(true);
                                    break;
                                case "CrearCuenta":
                                    Log.e("msg","Mensaje de peticion (CrearCuenta)");
                                    ((AplicacionPrincipal) context.getApplicationContext()).setCrearCuenta(true);
                                    break;
                                case "GetGrupoPorIntegrante":
                                    Log.e("msg","Mensaje de peticion (GetGrupoPorIntegrante)");
                                    ((AplicacionPrincipal) context.getApplicationContext()).setRecuperarGrupos(true);
                                    break;
                            }

                            writer.println(Serializacion.Serializar((MsgDBPeticion) arg[0]));
                            Log.e("msg","Envio el mensaje de peticion");
                            break;
                        case "MsgDBRespuesta":
                            writer.println(Serializacion.Serializar((MsgDBRespuesta) arg[0]));
                            break;
                        case "MsgNotificacion":
                            writer.println(Serializacion.Serializar((MsgNotificacion) arg[0]));
                            break;
                    }
                    writer.flush();

                }
            }
        } catch (Exception e) {
            e.printStackTrace();
            Log.e("msg", " se rompio en ServicioEnviar Excepcion: " + e.toString());
        }

        return null;
    }

    @Override
    public void onLocationChanged(Location location) {
        
        try {
            if(global.getSocket() != null) {
                if (global.getSocket().isConnected()) {

                    PrintWriter writer = new PrintWriter(global.getSocket().getOutputStream());

                    ArrayList<String> arrayDestino = new ArrayList<String>();
                    ArrayList<Grupo> grupos = global.getGrupos();
                    for (int i = 0; i < grupos.size(); i++)
                    {
                        arrayDestino.add(grupos.get(i).getNombre());
                    }
                        writer.println(Serializacion.Serializar(new MsgLocalizacion(arrayDestino, global.getCuenta().getUsuario(), "2016-10-27", location.getLatitude() + "", location.getLongitude() + "")));
                        writer.flush();

                        Log.e("msg", "Se envio un punto...");


                }
            }
        } catch (Exception e) {
            e.printStackTrace();
            Log.e("msg", "Se rompio cuando enviaba puntos");
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
