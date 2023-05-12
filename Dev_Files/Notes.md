# Notes für BeneTool

## **Notizen**

### **Rechnen von Schnitt könnte so implementiert werden**

Jedes Prüfung bekommt eine Wertung (standart wert '1'). Dieser Wert wird bei der Schnitterechnung respektiere.

Zuerst müssen alle Wertungen *normalisiert* werden:
```C#
float[] WertungenNormalisieren(float[] _wertungen){
    float[] _ausgabe = new float[_wertungen.length];
    float _total = 0;

    foreach(int w in _wertungen){
        _total += _wertungen;
    }

    for(int i = 0; i < _wertungen.length; i++) {
        _ausgabe[i] = _wertungen[i] / _total;
    }

    return _ausgabe;
}
```

Bei der Berechnung des Schnitts können dan die Wertungen verwendet werden:
```C#
float Schnitt(float[] _noten, float[] _wertungen) {
    float _ausgabe = 0;

    for(int i = 0; i < _noten.length; i++) {
        _ausgabe += _noten[i] * _wertungen[i];
    }

    return _ausgabe;
}
```
Dies würde den Schnitt mit Respekt zu den Wertungen errechnen.
