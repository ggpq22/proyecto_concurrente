package com.example.admin.pruebatracking.IU;

import android.Manifest;
import android.content.Context;
import android.content.DialogInterface;
import android.content.pm.PackageManager;
import android.graphics.drawable.AnimationDrawable;
import android.location.LocationManager;
import android.os.Bundle;
import android.support.v4.app.ActivityCompat;
import android.support.v4.app.Fragment;
import android.util.Log;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.Button;
import android.widget.EditText;
import android.widget.ImageView;
import android.widget.TextView;

import com.example.admin.pruebatracking.Cliente;
import com.example.admin.pruebatracking.R;

import java.io.PrintWriter;
import java.net.Socket;


public class FragmentGps extends Fragment {

    private Button btnLocalizacion;
    private ImageView imageView;
    TextView response;
    EditText editTextAddress, editTextPort;
    private AnimationDrawable savinAnimation;
    Context context;
    LocationManager manager;
    Cliente listener;

    @Override
    public View onCreateView(LayoutInflater inflater, ViewGroup container,
                             Bundle savedInstanceState) {
        View view = inflater.inflate(R.layout.fragment_gps, container, false);

        context = getActivity();

        imageView = (ImageView)view.findViewById(R.id.animacion);
        editTextAddress = (EditText) view.findViewById(R.id.textIP);
        editTextPort = (EditText) view.findViewById(R.id.textPuerto);
        response = (TextView) view.findViewById(R.id.respuesta);
        imageView.setBackgroundResource(R.drawable.animacion_desplazamiento);
        savinAnimation = (AnimationDrawable)imageView.getBackground();

        btnLocalizacion = (Button)view.findViewById(R.id.btnEnviarLocalizacion);

        btnLocalizacion.setOnClickListener(
                new View.OnClickListener() {
                    @Override
                    public void onClick(View v) {
                        switch (btnLocalizacion.getText().toString())
                        {
                            case "ENVIAR LOCALIZACIÓN":
                                btnLocalizacion.setText("STOP LOCALIZACIÓN");
                                btnLocalizacion.setCompoundDrawablesWithIntrinsicBounds(R.drawable.ic_stop1, 0, 0, 0);

                                manager = (LocationManager)getActivity().getSystemService(Context.LOCATION_SERVICE);

                                listener = new Cliente(btnLocalizacion, savinAnimation, getActivity(), editTextAddress.getText().toString(), Integer.parseInt(editTextPort.getText().toString()), response);
                                long tiempo = 1000;
                                float distancia = (float)0.1;

                                if (ActivityCompat.checkSelfPermission(getActivity(), Manifest.permission.ACCESS_FINE_LOCATION) != PackageManager.PERMISSION_GRANTED && ActivityCompat.checkSelfPermission(getActivity(), Manifest.permission.ACCESS_COARSE_LOCATION) != PackageManager.PERMISSION_GRANTED) {
                                    //Requiere permisos para Android 6.0
                                    Log.e("Location", "No se tienen permisos necesarios!, se requieren.");
                                    ActivityCompat.requestPermissions(getActivity(), new String[]{Manifest.permission.ACCESS_FINE_LOCATION, Manifest.permission.ACCESS_COARSE_LOCATION}, 225);
                                    return;
                                }else{
                                    Log.i("Location", "Permisos necesarios OK!.");
                                    manager.requestLocationUpdates(LocationManager.GPS_PROVIDER, tiempo, distancia, listener);
                                }

                                listener.execute();
                                savinAnimation.start();
                                break;
                            case "STOP LOCALIZACIÓN":
                                btnLocalizacion.setText("ENVIAR LOCALIZACIÓN");
                                btnLocalizacion.setCompoundDrawablesWithIntrinsicBounds(R.drawable.ic_localizacion, 0, 0, 0);
                                listener.cancel(true);
                                manager.removeUpdates(listener);
                                savinAnimation.stop();
                        }
                    }
                }

        );


        return view;
    }
}