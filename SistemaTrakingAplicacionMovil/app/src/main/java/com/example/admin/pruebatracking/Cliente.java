package com.example.admin.pruebatracking;

import android.Manifest;
import android.app.Activity;
import android.app.AlertDialog;
import android.content.Context;
import android.content.DialogInterface;
import android.content.Intent;
import android.content.pm.PackageManager;
import android.graphics.drawable.AnimationDrawable;
import android.location.LocationManager;
import android.os.AsyncTask;
import android.os.StrictMode;
import android.provider.Settings;
import android.support.v4.app.ActivityCompat;
import android.util.Log;
import android.widget.Button;
import android.widget.TextView;
import android.widget.Toast;

import com.example.admin.pruebatracking.Mensajes.Mensaje;
import com.example.admin.pruebatracking.Mensajes.MsgConexion;
import com.example.admin.pruebatracking.Serializacion.Serializacion;

import java.io.BufferedReader;
import java.io.InputStreamReader;
import java.io.PrintWriter;
import java.net.Socket;
import java.util.ArrayList;


public class Cliente extends AsyncTask<Void, Void, Void>{

    Context context;
    //Button btnLocalizacion;
    //AnimationDrawable savinAnimation;
    Socket socket = null;
    boolean conectado = false;
    PrintWriter writer;
    BufferedReader reader;
    long tiempo;
    float distancia;
    LocationManager manager = null;
    ServicioEnviar servicioEnviar = null;
    Thread servicioRecibir;
    ArrayList<String> TO;
    String FROM;
    String fecha;
    AplicacionPrincipal global;

    public Cliente(Context context, ArrayList<String> TO, String FROM, String fecha) {
        this.context = context;

        servicioEnviar = new ServicioEnviar(context);
        servicioRecibir = new Thread(new ServicioRecibir(context));
        manager = (LocationManager)context.getSystemService(Context.LOCATION_SERVICE);
        tiempo = 3;
        distancia = 1;

        this.TO = TO;
        this.FROM = FROM;
        this.fecha = fecha;

        global = ((AplicacionPrincipal) context.getApplicationContext());
    }

    protected Void doInBackground(Void... arg0) {
        try {
            Log.e("msg","entro en abrir conexion");
            socket = new Socket("10.75.60.122", 8999);
            writer = new PrintWriter(socket.getOutputStream());
            reader = new BufferedReader(new InputStreamReader(socket.getInputStream()));
            Log.e("msg","creo el socket");

            writer.println(Serializacion.Serializar(new MsgConexion(TO, FROM, fecha, "conectar")));
            writer.flush();

            Log.e("msg", "esperando respuesta");
            String jsonConexion = reader.readLine();
            Log.e("msg", "leyo respuesta");

            Log.i("msg", jsonConexion);
            MsgConexion msj = (MsgConexion) Serializacion.Deserealizar(jsonConexion);
            Log.i("msg", "ya deserialice " + msj.getMensaje());
            if (msj != null) {
                Log.e("msg", "SE CONECTO AL SERVIDOR CON EXITO");

                global.setSocket(socket);
                global.setConectado(true);
                recibirMensajes();

            } else {
                Log.e("msg", "ERROR AL CONECTARSE AL SERVIDOR");
            }


        } catch (Exception e) {
            e.printStackTrace();
            Log.e("msg", " se rompio en Cliente Excepcion: " + e.toString());
        }

        return null;
    }

    public void cerrarConexion()
    {
        try {
            writer.close();
            reader.close();
            socket.close();
        }
        catch (Exception e)
        {
            e.printStackTrace();
        }
    }

    public void iniciarLocalizacion()
    {
        if (manager.isProviderEnabled(LocationManager.GPS_PROVIDER)){

            if(manager != null && servicioEnviar!= null) {
                if (ActivityCompat.checkSelfPermission(context, Manifest.permission.ACCESS_FINE_LOCATION) != PackageManager.PERMISSION_GRANTED && ActivityCompat.checkSelfPermission(context, Manifest.permission.ACCESS_COARSE_LOCATION) != PackageManager.PERMISSION_GRANTED) {
                    //Requiere permisos para Android 6.0
                    Log.e("Location", "No se tienen permisos necesarios!, se requieren.");
                    ActivityCompat.requestPermissions((Activity) context, new String[]{Manifest.permission.ACCESS_FINE_LOCATION, Manifest.permission.ACCESS_COARSE_LOCATION}, 225);
                } else {
                    global.setEstadoGps(true);
                    Log.i("Location", "Permisos necesarios OK!.");
                    manager.requestLocationUpdates(LocationManager.GPS_PROVIDER, tiempo, distancia, servicioEnviar);
                }
            }
        }else{
            global.setEstadoGps(false);
            showGPSDisabledAlertToUser();
        }
    }

    public void pararLocalizacion()
    {
        if(manager != null && servicioEnviar != null) {
            if (ActivityCompat.checkSelfPermission(context, Manifest.permission.ACCESS_FINE_LOCATION) != PackageManager.PERMISSION_GRANTED && ActivityCompat.checkSelfPermission(context, Manifest.permission.ACCESS_COARSE_LOCATION) != PackageManager.PERMISSION_GRANTED) {
                //Requiere permisos para Android 6.0
                Log.e("Location", "No se tienen permisos necesarios!, se requieren.");
                ActivityCompat.requestPermissions((Activity) context, new String[]{Manifest.permission.ACCESS_FINE_LOCATION, Manifest.permission.ACCESS_COARSE_LOCATION}, 225);
                manager.removeUpdates(servicioEnviar);
            } else {
                Log.i("Location", "Permisos necesarios OK!.");
                manager.removeUpdates(servicioEnviar);
            }
        }
    }

    private void showGPSDisabledAlertToUser(){
        AlertDialog.Builder alertDialogBuilder = new AlertDialog.Builder(global.getContext());
        alertDialogBuilder.setMessage("GPS is disabled in your device. Would you like to enable it?")
                .setCancelable(false)
                .setPositiveButton("Goto Settings Page To Enable GPS",
                        new DialogInterface.OnClickListener(){
                            public void onClick(DialogInterface dialog, int id){
                                Intent callGPSSettingIntent = new Intent(
                                        android.provider.Settings.ACTION_LOCATION_SOURCE_SETTINGS);
                                global.getContext().startActivity(callGPSSettingIntent);
                            }
                        });
        alertDialogBuilder.setNegativeButton("Cancel",
                new DialogInterface.OnClickListener(){
                    public void onClick(DialogInterface dialog, int id){
                        dialog.cancel();
                    }
                });
        AlertDialog alert = alertDialogBuilder.create();
        alert.show();
    }

    public void recibirMensajes()
    {
        if(servicioRecibir != null) {
            servicioRecibir.start();
        }
    }

    public void pararRecibirMensajes()
    {
        if(servicioRecibir != null) {
            Log.e("msg", "INTERRUMPIENDO SERVICIO RECIBIR");
            servicioRecibir.interrupt();
            servicioRecibir = null;
        }
    }

    public void enviarMensajes(Mensaje msg)
    {
        if(servicioEnviar != null){
            servicioEnviar.execute(msg);
        }
    }

    @Override
    protected void onPostExecute(Void result) {
        super.onPostExecute(result);
    }
}
