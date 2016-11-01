package com.example.admin.pruebatracking;

import java.io.BufferedReader;
import java.io.InputStreamReader;
import java.io.PrintWriter;
import java.net.Socket;

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

import com.example.admin.pruebatracking.Mensajes.MsgConexion;
import com.example.admin.pruebatracking.Mensajes.MsgLocalizacion;
import com.example.admin.pruebatracking.Serializacion.Serializacion;



public class Cliente extends AsyncTask<Void, Void, Void> implements LocationListener {

    Context context;
    String addr;
    int port;
    String respuesta = "";
    TextView textRespuesta;
    AnimationDrawable savinAnimation;
    Button btnLocalizacion;
    Socket socket;

    public Cliente( Button btnLocalizacion, AnimationDrawable savinAnimation, final Context context, String addr, int port, TextView textRespuesta) {
        this.addr = addr;
        this.port = port;
        this.textRespuesta = textRespuesta;
        this.context = context;
        this.savinAnimation = savinAnimation;
        this.btnLocalizacion = btnLocalizacion;

    }

    @Override
    protected Void doInBackground(Void... arg0) {

        try {
                socket = new Socket(addr,port);
                PrintWriter writer = new PrintWriter(socket.getOutputStream());
                BufferedReader r = new BufferedReader(new InputStreamReader(socket.getInputStream()));

                writer.println(Serializacion.Serializar(new MsgConexion("escritorio", "yo", "2016-10-18", "conectar")));
                writer.flush();

                Log.i("msg","esperando respuesta");
                String jsonConexion = r.readLine();
                Log.i("msg","leyo respuesta");

                Log.i("msg", jsonConexion);
                MsgConexion msj = (MsgConexion) Serializacion.Deserealizar(jsonConexion);
                Log.i("msg", "ya deserialice " + msj.getMensaje());
                if(msj != null) {
                    Log.i("msg", "SE CONECTO AL SERVIDOR CON EXITO");
                    ((Activity)context).runOnUiThread(new Runnable() {
                        @Override
                        public void run() {
                            Toast.makeText(context, "SE CONECTO AL SERVIDOR CON EXITO", Toast.LENGTH_LONG).show();
                        }
                    });

                    while (socket.isConnected())
                    {
                        Thread.sleep(3000);
                    }
                }
                else
                {
                    Log.i("msg", "ERROR AL CONECTARSE AL SERVIDOR");
                    ((Activity)context).runOnUiThread(new Runnable() {
                        @Override
                        public void run() {
                            Toast.makeText(context, "ERROR AL CONECTARSE AL SERVIDOR",Toast.LENGTH_LONG).show();
                        }
                    });
                }

            Log.i("msg", "CONEXION CERRADA");
            ((Activity)context).runOnUiThread(new Runnable() {
                @Override
                public void run() {
                    Toast.makeText(context, "CONEXION CERRADA", Toast.LENGTH_LONG).show();
            }
            });
            savinAnimation.stop();

            socket.close();
            writer.close();
        } catch (Exception e) {
            e.printStackTrace();
            savinAnimation.stop();
            respuesta = "Error! "+e.toString();

        }
        return null;
    }


    @Override
    protected void onPostExecute(Void result) {
        textRespuesta.setText(respuesta);
        btnLocalizacion.setText("ENVIAR LOCALIZACIÓN");
        btnLocalizacion.setCompoundDrawablesWithIntrinsicBounds(R.drawable.ic_localizacion, 0, 0, 0);
        super.onPostExecute(result);
    }


    @Override
    public void onLocationChanged(Location location) {

        try {
            if(socket != null) {
                if (socket.isConnected()) {

                    PrintWriter writer = new PrintWriter(socket.getOutputStream());
                    BufferedReader r = new BufferedReader(new InputStreamReader(socket.getInputStream()));

                    writer.println(Serializacion.Serializar(new MsgLocalizacion("yo", "yo", "2016-10-27", location.getLatitude() + "", location.getLongitude() + "")));
                    writer.flush();

                    //String jsonLocalizacion = r.readLine();
                    //MsgLocalizacion msjLocalizacion = (MsgLocalizacion) Serializacion.Deserealizar(jsonLocalizacion);

                    //Log.i("msg", "LLego: Latitud " + msjLocalizacion.getLatitud() + " Longitud: " + msjLocalizacion.getLongitud());
                    //Toast.makeText(context, "LLego: Latitud " + msjLocalizacion.getLatitud() + " Longitud: " + msjLocalizacion.getLongitud(), Toast.LENGTH_LONG).show();

                }
            }
        } catch (Exception e) {
            e.printStackTrace();
            savinAnimation.stop();

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
