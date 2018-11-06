package com.example.admin.pruebatracking.IU;

import android.Manifest;
import android.app.Activity;
import android.content.Context;
import android.content.pm.PackageManager;
import android.graphics.drawable.AnimationDrawable;
import android.location.LocationManager;
import android.os.Bundle;
import android.support.v4.app.ActivityCompat;
import android.support.v4.app.Fragment;
import android.text.format.DateFormat;
import android.util.Log;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.Button;
import android.widget.EditText;
import android.widget.ImageView;
import android.widget.TextView;
import android.widget.Toast;

import com.example.admin.pruebatracking.AplicacionPrincipal;
import com.example.admin.pruebatracking.Cliente;
import com.example.admin.pruebatracking.Entidades.Cuenta;
import com.example.admin.pruebatracking.Entidades.DBEntidad;
import com.example.admin.pruebatracking.Entidades.Grupo;
import com.example.admin.pruebatracking.Mensajes.MsgDBPeticion;
import com.example.admin.pruebatracking.Mensajes.MsgDBRespuesta;
import com.example.admin.pruebatracking.ServicioEnviar;
import com.example.admin.pruebatracking.R;

import java.util.ArrayList;
import java.util.UUID;


public class FragmentGps extends Fragment {

    private Button btnLocalizacion;
    private ImageView imageView;
    private AnimationDrawable savinAnimation;
    Context context;
    Cliente cliente;
    AplicacionPrincipal global;

    @Override
    public View onCreateView(LayoutInflater inflater, ViewGroup container,
                             Bundle savedInstanceState) {
        View view = inflater.inflate(R.layout.fragment_gps, container, false);

        context = getActivity();
        global = ((AplicacionPrincipal) context.getApplicationContext());

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
                                if(global.getSocket().isConnected()) {
                                    ArrayList<String> arrayDestino = new ArrayList<String>();
                                    ArrayList<Grupo> grupos = global.getGrupos();
                                    for (int i = 0; i < grupos.size(); i++)
                                    {
                                        arrayDestino.add(grupos.get(i).getNombre());
                                    }

                                    if(arrayDestino.size() > 0) {

                                        String fecha = (DateFormat.format("yyyy-MM-dd", new java.util.Date()).toString());

                                        cliente = new Cliente(context, arrayDestino, ((AplicacionPrincipal) context.getApplicationContext()).getCuenta().getUsuario(), fecha);
                                        cliente.iniciarLocalizacion();

                                        if(global.getEstadoGps()) {
                                            btnLocalizacion.setText("STOP LOCALIZACIÓN");
                                            btnLocalizacion.setCompoundDrawablesWithIntrinsicBounds(R.drawable.ic_stop1, 0, 0, 0);
                                            savinAnimation.start();
                                        }
                                    }
                                    else {

                                        Toast.makeText(getContext(), "No hay grupos asociados para enviar localización", Toast.LENGTH_LONG).show();

                                    }
                                }
                                else
                                {
                                    Toast.makeText(context, "NO SE PUDO CONECTAR AL SERVIDOR", Toast.LENGTH_LONG).show();
                                }

                                break;
                            case "STOP LOCALIZACIÓN":
                                btnLocalizacion.setText("ENVIAR LOCALIZACIÓN");
                                btnLocalizacion.setCompoundDrawablesWithIntrinsicBounds(R.drawable.ic_localizacion, 0, 0, 0);
                                savinAnimation.stop();
                                cliente.pararLocalizacion();
                                ((AplicacionPrincipal) context.getApplicationContext()).setConectado(false);

                        }
                    }
                }

        );


        return view;
    }
}