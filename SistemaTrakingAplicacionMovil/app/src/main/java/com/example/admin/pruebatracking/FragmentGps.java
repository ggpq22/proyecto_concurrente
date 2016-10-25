package com.example.admin.pruebatracking;

import android.content.DialogInterface;
import android.graphics.drawable.AnimationDrawable;
import android.os.Bundle;
import android.support.v4.app.Fragment;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.Button;
import android.widget.ImageView;


public class FragmentGps extends Fragment {

    private Button btn;
    private ImageView imageView;
    private AnimationDrawable savinAnimation;

    @Override
    public View onCreateView(LayoutInflater inflater, ViewGroup container,
                             Bundle savedInstanceState) {
        View view = inflater.inflate(R.layout.fragment_gps, container, false);


        imageView = (ImageView)view.findViewById(R.id.animacion);
        imageView.setBackgroundResource(R.drawable.animacion_desplazamiento);
        savinAnimation = (AnimationDrawable)imageView.getBackground();

        btn = (Button)view.findViewById(R.id.button);
        btn.setOnClickListener(
                new View.OnClickListener() {
                    @Override
                    public void onClick(View v) {
                        switch (btn.getText().toString())
                        {
                            case "ACTIVAR SEGUIMIENTO":
                                btn.setText("DESACTIVAR SEGUIMIENTO");
                                savinAnimation.start();
                                break;
                            case "DESACTIVAR SEGUIMIENTO":
                                btn.setText("ACTIVAR SEGUIMIENTO");
                                savinAnimation.stop();
                        }
                    }
                }

        );


        return view;
    }
}