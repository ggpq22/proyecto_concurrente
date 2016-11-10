package com.example.admin.pruebatracking.IU;

import android.Manifest;
import android.content.Context;
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
import com.example.admin.pruebatracking.Entidades.Cuenta;
import com.example.admin.pruebatracking.Entidades.DBEntidad;
import com.example.admin.pruebatracking.Mensajes.MsgDBPeticion;
import com.example.admin.pruebatracking.ServicioEnviar;
import com.example.admin.pruebatracking.R;

import java.util.ArrayList;


public class FragmentGps extends Fragment {

    private Button btnLocalizacion;
    private ImageView imageView;
    private AnimationDrawable savinAnimation;
    Context context;
    Cliente cliente;

    @Override
    public View onCreateView(LayoutInflater inflater, ViewGroup container,
                             Bundle savedInstanceState) {
        View view = inflater.inflate(R.layout.fragment_gps, container, false);

        context = getActivity();

        imageView = (ImageView)view.findViewById(R.id.animacion);
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
                                /*btnLocalizacion.setText("STOP LOCALIZACIÓN");
                                btnLocalizacion.setCompoundDrawablesWithIntrinsicBounds(R.drawable.ic_stop1, 0, 0, 0);
                                cliente = new Cliente(btnLocalizacion, savinAnimation, context, "192.168.0.100", 8999);
                                cliente.execute();
                                cliente.iniciarLocalizacion();
                                cliente.recibirMensajes();
                                ArrayList<DBEntidad> arrayCuenta = new ArrayList<DBEntidad>();
                                Cuenta cuenta = new Cuenta(1, "pablo", "123");
                                cuenta.setUsuario("pablo");
                                cuenta.setPass("123");
                                arrayCuenta.add(cuenta);
                                cliente.enviarMensajes(new MsgDBPeticion("yo", "yo", "2016-10-18", "GetGrupoPorIntegrante", arrayCuenta));
                                */
                                savinAnimation.start();

                                break;
                            case "STOP LOCALIZACIÓN":
                                btnLocalizacion.setText("ENVIAR LOCALIZACIÓN");
                                btnLocalizacion.setCompoundDrawablesWithIntrinsicBounds(R.drawable.ic_localizacion, 0, 0, 0);
                                /*cliente.cerrarConexion();
                                cliente.pararLocalizacion();
                                cliente.pararRecibirMensajes();*/
                                savinAnimation.stop();
                        }
                    }
                }

        );


        return view;
    }
}