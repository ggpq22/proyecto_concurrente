package com.example.admin.pruebatracking;

import java.io.BufferedReader;
import java.io.InputStreamReader;
import java.io.PrintWriter;
import java.net.Socket;

import android.content.Context;
import android.content.pm.PackageManager;
import android.location.Location;
import android.location.LocationManager;
import android.location.LocationProvider;
import android.os.AsyncTask;
import android.support.v4.content.ContextCompat;
import android.util.Log;
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

    public Cliente(Context context, String addr, int port, TextView textRespuesta) {
        this.addr = addr;
        this.port = port;
        this.textRespuesta = textRespuesta;
        this.context = context;
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
                Log.i("Tarea Deserializar", "ya deserialice");
                if(msj != null)
                {
                    Toast.makeText(context, "SE CONECTO AL SERVIDOR CON EXITO",Toast.LENGTH_LONG).show();
                    while(socket.isConnected()) {

                        LocationManager manager = (LocationManager)context.getSystemService(Context.LOCATION_SERVICE);
                        LocationProvider proveedor = manager.getProvider(LocationManager.GPS_PROVIDER);
                        Location posicion = null;
                        if ( ContextCompat.checkSelfPermission(context, android.Manifest.permission.ACCESS_COARSE_LOCATION) == PackageManager.PERMISSION_GRANTED ) {
                            while (socket.isConnected()) {
                                posicion = manager.getLastKnownLocation(LocationManager.GPS_PROVIDER);

                                writer.println(Serializacion.Serializar(new MsgLocalizacion("yo", "yo", "2016-10-27", posicion.getLatitude() + "", posicion.getLongitude() + "")));
                                writer.flush();

                                String jsonLocalizacion = r.readLine();
                                MsgLocalizacion msjLocalizacion = (MsgLocalizacion) Serializacion.Deserealizar(jsonLocalizacion);
                                Toast.makeText(context,"LLego: Latitud " + msjLocalizacion.getLatitud() + " Longitud: " + msjLocalizacion.getLongitud(),Toast.LENGTH_LONG).show();

                                Thread.sleep(5000);
                            }
                        }
                        else
                        {
                            Toast.makeText(context, "NO TIENE PERMISOS PARA ACCEDER AL GPS",Toast.LENGTH_LONG).show();
                        }

                    }
                }
                else
                {
                    Toast.makeText(context, "ERROR AL CONECTARSE AL SERVIDOR",Toast.LENGTH_LONG).show();
                }



            Log.i("msg",respuesta);


            socket.close();
            writer.close();
        } catch (Exception e) {
            e.printStackTrace();
            respuesta = "Error! "+e.toString();
        }
        return null;
    }


    @Override
    protected void onPostExecute(Void result) {
        textRespuesta.setText(respuesta);
        super.onPostExecute(result);
    }

}
