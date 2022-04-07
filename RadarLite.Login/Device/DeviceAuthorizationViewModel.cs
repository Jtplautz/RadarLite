// Copyright (c) Duende Software. All rights reserved.
// See LICENSE in the project root for license information.


using RadarLite.Login.Consent;

namespace RadarLite.Login.Device;

public class DeviceAuthorizationViewModel : ConsentViewModel {
    public string UserCode { get; set; }
    public bool ConfirmUserCode { get; set; }
}
