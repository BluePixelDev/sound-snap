# `AudioSourceData`
**Namespace:** `BP.Audipool`  
**Type:** `class`(sealed, serializable)

A serializable preset object for configuring Unity `AudioSource` instances.
`AudioSourceData` can be reused, copied from existing sources, and applied
to pooled or runtime-created audio sources.

This type exposes only high-level configuration intended for public API usage.

## Properties

### Resource & Routing

```csharp
public AudioResource Resource { get; }
public AudioMixerGroup MixerGroup { get; }
```

The audio resource and optional mixer group assigned to the source.

### Audio Parameters

```csharp
public int Priority { get; set; }
public float Volume { get; set; }
public float Pitch { get; set; }
public float PanStereo { get; set; }
public float SpatialBlend { get; set; }
```

Controls playback priority, loudness, pitch, stereo panning, and 2D/3D blend.

All setters clamp values to Unity-safe ranges.

### 3D Parameters

```csharp
public float DopplerLevel { get; set; }
public float Spread { get; set; }
public AudioRolloffMode RolloffMode { get; }
public float MinDistance { get; }
public float MaxDistance { get; }
```

Controls 3D spatial behavior, distance attenuation, and Doppler effects.

## Constructors

### `AudioSourceData()`

Creates a new settings instance with sensible defaults suitable for general use.

### `AudioSourceData(AudioSource source)`

Creates a new settings instance and copies all supported configuration
from an existing `AudioSource`.

## Methods

### `CopyFromSource(AudioSource source)`

```csharp
public void CopyFromSource(AudioSource source)
```

Copies all supported audio-related properties from the given `AudioSource`
into this settings object.

* Logs an error if `source` is null

### `ApplyToSource(AudioSource source)`

```csharp
public void ApplyToSource(AudioSource source)
```

Applies all stored settings to the given `AudioSource`.

* Logs an error if `source` is null
* Overwrites all relevant AudioSource parameters

## Usage Examples

### Creating from an AudioSource

```csharp
var settings = new AudioSourceData(existingSource);
```

### Applying to a Pooled Source

```csharp
settings.ApplyToSource(audioSource);
```
