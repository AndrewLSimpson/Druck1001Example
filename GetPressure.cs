//Use the Ivi.Visa referenc
using Ivi.Visa;

// Put this method somewhere in your code and then call it where you need it.
// it will return the pressure as a string, so you may want to + "mmHg" after you call it, for example:
// Console.WriteLine(PressuremBar() + " mmHg");

public string PressuremBar()
        {
            // Connect to the druck using an IVisaSession and globalresourcemanager
            using (IVisaSession res = GlobalResourceManager.Open("TCPIP::10.115.46.84::inst0::INSTR", AccessModes.ExclusiveLock, 2000))
            {
                //if the resource is a IMessageBasedSession (which it should be)
                if (res is IMessageBasedSession session)
                {
                    // Ensure termination character is enabled as here in example we use a SOCKET connection.
                    session.TerminationCharacterEnabled = true;
                    // Request information about the druck.
                    session.FormattedIO.WriteLine(":SENSe:PRESsure?");
                    //Capture the information
                    string idn = session.FormattedIO.ReadLine();
                    //Set the index of the payload
                    int index = idn.IndexOf(' ');
                    //only get these specific characters, we're only interested in the numbers.
                    string pressure = idn.Substring(index + 1, 7);
                    //Return the pressure
                    return pressure;
                }
                else
                {
                    return "Offline";
                }
            }
        }
