package com.example.admin.pruebatracking;

import android.Manifest;
import android.app.Activity;
import android.content.Context;
import android.content.pm.PackageManager;
import android.graphics.drawable.AnimationDrawable;
import android.location.LocationManager;
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

/**
 * Created by Admin on 3/11/2016.
 */
public class Cliente {

    Context context;
    String addr;
    int port;
    Button btnLocalizacion;
    AnimationDrawable savinAnimation;
    Socket socket = null;
    boolean conectado = false;
    PrintWriter writer;
    BufferedReader reader;
    long tiempo;
    float distancia;
    LocationManager manager = null;
    ServicioEnviar servicioEnviar = null;
    ServicioRecibir servicioRecibir = null;

    public Cliente(Button btnLocalizacion, AnimationDrawable savinAnimation, Context context, String addr, int port) {
        this.addr = addr;
        this.port = port;
        this.context = context;
        this.btnLocalizacion = btnLocalizacion;
        this.savinAnimation = savinAnimation;

        servicioEnviar = new ServicioEnviar(socket, context, conectado);
        servicioRecibir = new ServicioRecibir(socket, context, conectado);
        manager = (LocationManager)context.getSystemService(Context.LOCATION_SERVICE);
        tiempo = 5000;
        distancia = 5;
    }

    public void iniciarConexion()
    {
        try {
            socket = new Socket(addr, port);
            writer = new PrintWriter(socket.getOutputStream());
            reader = new BufferedReader(new InputStreamReader(socket.getInputStream()));

            writer.println(Serializacion.Serializar(new MsgConexion("yo", "yo", "2016-10-18", "conectar")));
            writer.flush();

            Log.i("msg", "esperando respuesta");
            String jsonConexion = reader.readLine();
            Log.i("msg", "leyo respuesta");

            Log.i("msg", jsonConexion);
            MsgConexion msj = (MsgConexion) Serializacion.Deserealizar(jsonConexion);
            Log.i("msg", "ya deserialice " + msj.getMensaje());
            if (msj != null) {
                Log.i("msg", "SE CONECTO AL SERVIDOR CON EXITO");
                conectado = true;
                ((Activity) context).runOnUiThread(new Runnable() {
                    @Override
                    public void run() {
                        Toast.makeText(context, "SE CONECTO AL SERVIDOR CON EXITO", Toast.LENGTH_LONG).show();
                    }
                });

            } else {
                Log.i("msg", "ERROR AL CONECTARSE AL SERVIDOR");
                ((Activity) context).runOnUiThread(new Runnable() {
                    @Override
                    public void run() {
                        Toast.makeText(context, "ERROR AL CONECTARSE AL SERVIDOR", Toast.LENGTH_LONG).show();
                    }
                });
            }

            Log.i("msg", "CONEXION CERRADA");
            ((Activity) context).runOnUiThread(new Runnable() {
                @Override
                public void run() {
                    Toast.makeText(context, "CONEXION CERRADA", Toast.LENGTH_LONG).show();
                }
            });

        } catch (Exception e) {
            e.printStackTrace();
            ((Activity) context).runOnUiThread(new Runnable() {
                @Override
                public void run() {
                    Toast.makeText(context, "ERROR EN EL SERVIDOR ", Toast.LENGTH_LONG).show();
                }
            });
        }
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
        if(manager != null && servicioEnviar!= null) {
            if (ActivityCompat.checkSelfPermission(context, Manifest.permission.ACCESS_FINE_LOCATION) != PackageManager.PERMISSION_GRANTED && ActivityCompat.checkSelfPermission(context, Manifest.permission.ACCESS_COARSE_LOCATION) != PackageManager.PERMISSION_GRANTED) {
                //Requiere permisos para Android 6.0
                Log.e("Location", "No se tienen permisos necesarios!, se requieren.");
                ActivityCompat.requestPermissions((Activity) context, new String[]{Manifest.permission.ACCESS_FINE_LOCATION, Manifest.permission.ACCESS_COARSE_LOCATION}, 225);
                manager.requestLocationUpdates(LocationManager.GPS_PROVIDER, tiempo, distancia, servicioEnviar);
            } else {
                Log.i("Location", "Permisos necesarios OK!.");
                manager.requestLocationUpdates(LocationManager.GPS_PROVIDER, tiempo, distancia, servicioEnviar);
            }
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

    public void recibirMensajes()
    {
        if(servicioRecibir != null) {
            servicioRecibir.execute();
        }
    }

    public void pararRecibirMensajes()
    {
        if(servicioRecibir != null) {
            servicioRecibir.cancel(true);
        }
    }

    public void enviarMensajes(Mensaje msg)
    {
        if(servicioEnviar != null){
            servicioEnviar.enviarMensaje(msg);
        }
    }

}
