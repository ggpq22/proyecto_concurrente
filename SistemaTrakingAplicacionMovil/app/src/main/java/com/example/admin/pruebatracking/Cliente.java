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
import android.location.LocationManager;
import android.location.LocationProvider;
import android.os.AsyncTask;
import android.support.v4.content.ContextCompat;
import android.util.Log;
import android.widget.Button;
import android.widget.TextView;
import android.widget.Toast;

import com.example.admin.pruebatracking.Mensajes.MsgConexion;
import com.example.admin.pruebatracking.Mensajes.MsgLocalizacion;
import com.example.admin.pruebatracking.Serializacion.Serializacion;



public class Cliente extends AsyncTask<Void, Void, Void> {

    Context context;
    String addr;
    int port;
    String respuesta = "";
    TextView textRespuesta;
    AnimationDrawable savinAnimation;
    Button btnLocalizacion;
    LocationManager manager;
    ListenerPosicion listener;
    Location posicion;
    long tiempo = 5000;
    float distancia = 10;

    public Cliente(Button btnLocalizacion, AnimationDrawable savinAnimation, Context context, String addr, int port, TextView textRespuesta) {
        this.addr = addr;
        this.port = port;
        this.textRespuesta = textRespuesta;
        this.context = context;
        this.savinAnimation = savinAnimation;
        this.btnLocalizacion = btnLocalizacion;
    }

    @Override
    protected Void doInBackground(Void... arg0) {

        Socket socket = null;

        try {
            socket = new Socket( addr, port);
            PrintWriter writer = new PrintWriter(socket.getOutputStream());


            BufferedReader r = new BufferedReader(new InputStreamReader(socket.getInputStream()));

                writer.println(Serializacion.Serializar(new MsgConexion("yo", "yo", "2016-10-18", "conectar")));
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

                    manager = (LocationManager) context.getSystemService(Context.LOCATION_SERVICE);
                    //LocationProvider proveedor = manager.getProvider(LocationManager.GPS_PROVIDER);

                    listener = new ListenerPosicion(context);
                    tiempo = 5000;
                    distancia = 10;

                    ((Activity)context).runOnUiThread(new Runnable() {
                        @Override
                        public void run() {
                            manager.requestLocationUpdates(LocationManager.GPS_PROVIDER, tiempo, distancia, listener);
                        }
                    });


                        while (socket.isConnected()) {

                            posicion =  manager.getLastKnownLocation(LocationManager.GPS_PROVIDER);

                            writer.println(Serializacion.Serializar(new MsgLocalizacion("yo", "yo", "2016-10-27", posicion.getLatitude() + "", posicion.getLongitude() + "")));
                            writer.flush();

                            String jsonLocalizacion = r.readLine();
                            MsgLocalizacion msjLocalizacion = (MsgLocalizacion) Serializacion.Deserealizar(jsonLocalizacion);

                            Log.i("msg", "LLego: Latitud " + msjLocalizacion.getLatitud() + " Longitud: " + msjLocalizacion.getLongitud());
                            //Toast.makeText(context, "LLego: Latitud " + msjLocalizacion.getLatitud() + " Longitud: " + msjLocalizacion.getLongitud(), Toast.LENGTH_LONG).show();

                            Thread.sleep(5000);
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
            manager.removeUpdates(listener);

            socket.close();
            writer.close();
        } catch (Exception e) {
            e.printStackTrace();
            savinAnimation.stop();
            respuesta = "Error! "+e.toString();
            manager.removeUpdates(listener);
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

}
